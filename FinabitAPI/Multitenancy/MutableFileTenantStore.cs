using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace FinabitAPI.Multitenancy
{
    public sealed class MutableFileTenantStore : ITenantStore
    {
        private readonly string _filePath;
        private readonly SemaphoreSlim _gate = new(1, 1);
        private readonly ConcurrentDictionary<string, Tenant> _tenants = new(StringComparer.OrdinalIgnoreCase);
        private string? _defaultId;

        private sealed class PersistModel
        {
            public string? DefaultId { get; set; }
            public List<Tenant> Items { get; set; } = new();
        }

        public MutableFileTenantStore(IConfiguration cfg, IWebHostEnvironment env)
        {
            _filePath = Path.GetFullPath(Path.Combine(env.ContentRootPath, "tenants.json"));

            if (File.Exists(_filePath))
            {
                // 1) Prefer persisted tenants.json if present
                var json = File.ReadAllText(_filePath);
                var model = JsonSerializer.Deserialize<PersistModel>(json) ?? new();
                foreach (var t in model.Items) _tenants[t.Id] = t;
                _defaultId = model.DefaultId;
            }
            else
            {
                // 2) Try instance.settings.json first
                if (!LoadTenantsFromJson(Path.Combine(env.ContentRootPath, "instance.settings.json")))
                {
                    // 3) Then appsettings.json
                    LoadTenantsFromJson(Path.Combine(env.ContentRootPath, "appsettings.json"));
                }

                // 4) If still no default tenant, fallback to DefaultConnection
                if (_defaultId is null)
                {
                    var cs = ReadConnectionStringPreferringInstance(env.ContentRootPath);
                    if (!string.IsNullOrWhiteSpace(cs))
                    {
                        var fallback = new Tenant
                        {
                            Id = "default",
                            Name = "Default (connection string)",
                            ConnectionString = cs
                        };
                        _tenants[fallback.Id] = fallback;
                        _defaultId = fallback.Id;
                    }
                }

                // Persist an initial tenants.json so future edits have a home
                _ = SaveUnlockedAsync(CancellationToken.None);
            }
        }

        // ---- ITenantStore ----
        public Task<Tenant?> FindAsync(string tenantId, CancellationToken ct = default) =>
            Task.FromResult(_tenants.TryGetValue(tenantId, out var t) ? t : null);

        public Task<Tenant?> GetDefaultAsync(CancellationToken ct = default) =>
            Task.FromResult(_defaultId is not null && _tenants.TryGetValue(_defaultId, out var t) ? t : null);

        public Task<string?> GetDefaultIdAsync(CancellationToken ct = default) =>
            Task.FromResult(_defaultId);

        public Task<IReadOnlyCollection<Tenant>> GetAllAsync(CancellationToken ct = default) =>
            Task.FromResult<IReadOnlyCollection<Tenant>>(_tenants.Values.OrderBy(t => t.Id).ToArray());

        public async Task<bool> AddOrUpdateAsync(Tenant tenant, bool setAsDefault = false, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(tenant.Id)) return false;

            await _gate.WaitAsync(ct);
            try
            {
                _tenants[tenant.Id] = tenant;
                if (setAsDefault) _defaultId = tenant.Id;
                await SaveUnlockedAsync(ct);
                return true;
            }
            finally { _gate.Release(); }
        }

        public async Task<bool> RemoveAsync(string tenantId, CancellationToken ct = default)
        {
            await _gate.WaitAsync(ct);
            try
            {
                var removed = _tenants.TryRemove(tenantId, out _);
                if (removed && string.Equals(_defaultId, tenantId, StringComparison.OrdinalIgnoreCase))
                    _defaultId = _tenants.Keys.FirstOrDefault();
                await SaveUnlockedAsync(ct);
                return removed;
            }
            finally { _gate.Release(); }
        }

        // ---- helpers ----
        private bool LoadTenantsFromJson(string path)
        {
            if (!File.Exists(path)) return false;

            using var doc = JsonDocument.Parse(File.ReadAllText(path));
            if (!doc.RootElement.TryGetProperty("Tenants", out var tenantsNode))
                return false;

            bool any = false;

            if (tenantsNode.TryGetProperty("Items", out var items) && items.ValueKind == JsonValueKind.Array)
            {
                foreach (var el in items.EnumerateArray())
                {
                    var t = new Tenant
                    {
                        Id = el.TryGetProperty("Id", out var id) ? id.GetString() ?? "" : "",
                        Name = el.TryGetProperty("Name", out var name) ? name.GetString() ?? "" : "",
                        ConnectionString = el.TryGetProperty("ConnectionString", out var cs) ? cs.GetString() ?? "" : ""
                    };
                    if (!string.IsNullOrWhiteSpace(t.Id))
                    {
                        _tenants[t.Id] = t;
                        any = true;
                    }
                }
            }

            if (tenantsNode.TryGetProperty("DefaultId", out var def) && def.ValueKind == JsonValueKind.String)
                _defaultId = def.GetString();

            return any || _defaultId is not null;
        }

        private static string? ReadConnectionStringFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            using var doc = JsonDocument.Parse(File.ReadAllText(filePath));
            if (!doc.RootElement.TryGetProperty("ConnectionStrings", out var csNode))
                return null;

            if (csNode.TryGetProperty("DefaultConnection", out var def) && def.ValueKind == JsonValueKind.String)
                return def.GetString();

            if (csNode.TryGetProperty("strCnn", out var alt) && alt.ValueKind == JsonValueKind.String)
                return alt.GetString();

            return null;
        }

        private static string? ReadConnectionStringPreferringInstance(string contentRoot)
        {
            // Prefer instance.settings.json, then appsettings.json
            return ReadConnectionStringFromFile(Path.Combine(contentRoot, "instance.settings.json"))
                   ?? ReadConnectionStringFromFile(Path.Combine(contentRoot, "appsettings.json"));
        }

        private async Task SaveUnlockedAsync(CancellationToken ct)
        {
            var model = new PersistModel
            {
                DefaultId = _defaultId,
                Items = _tenants.Values.OrderBy(t => t.Id).ToList()
            };
            var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            await File.WriteAllTextAsync(_filePath, json, ct);
        }
    }
}
