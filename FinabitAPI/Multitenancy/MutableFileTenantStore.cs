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
            // Keep the existing file name
            _filePath = Path.GetFullPath(Path.Combine(env.ContentRootPath, "tenants.json"));

            // seed from config (Tenants:Items / Tenants:DefaultId) OR from tenants.json if present
            // Prefer the external tenants.json so runtime changes persist.
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var model = JsonSerializer.Deserialize<PersistModel>(json) ?? new();
                foreach (var t in model.Items) _tenants[t.Id] = t;
                _defaultId = model.DefaultId;
            }
            else
            {
                var list = cfg.GetSection("Tenants:Items").Get<List<Tenant>>() ?? new();
                foreach (var t in list) _tenants[t.Id] = t;
                _defaultId = cfg.GetValue<string>("Tenants:DefaultId");

                // Fallback to appsettings DefaultConnection
                if (_defaultId is null && cfg.GetConnectionString("DefaultConnection") is string cs)
                {
                    var fallback = new Tenant { Id = "appsettings", Name = "AppSettings Default", ConnectionString = cs };
                    _tenants[fallback.Id] = fallback;
                    _defaultId = fallback.Id;
                }

                // Immediately persist an initial file so future edits save somewhere.
                _ = SaveUnlockedAsync(CancellationToken.None);
            }
        }

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
                    _defaultId = _tenants.Keys.FirstOrDefault(); // pick any remaining or null
                await SaveUnlockedAsync(ct);
                return removed;
            }
            finally { _gate.Release(); }
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
