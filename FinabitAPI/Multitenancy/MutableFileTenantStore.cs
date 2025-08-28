using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // <-- add

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
                var json = File.ReadAllText(_filePath);
                var model = JsonSerializer.Deserialize<PersistModel>(json) ?? new();
                foreach (var t in model.Items.Select(FixupTenantFromConnStringIfNeeded))
                    _tenants[t.Id] = t;
                _defaultId = model.DefaultId;
            }
            else
            {
                // Prefer instance.settings.json; then appsettings.json
                if (!LoadTenantsFromJson(Path.Combine(env.ContentRootPath, "instance.settings.json")))
                    LoadTenantsFromJson(Path.Combine(env.ContentRootPath, "appsettings.json"));

                // If still no default tenant, fallback to DefaultConnection/strCnn
                if (_defaultId is null)
                {
                    var cs = ReadConnectionStringPreferringInstance(env.ContentRootPath);
                    if (!string.IsNullOrWhiteSpace(cs))
                    {
                        var (server, db) = ParseServerAndDb(cs);
                        var fallback = new Tenant
                        {
                            Id = "default",
                            Name = "Default",
                            ConnectionString = cs,
                            Server = server,
                            Database = db
                        };
                        _tenants[fallback.Id] = fallback;
                        _defaultId = fallback.Id;
                    }
                }

                // Persist initial file so edits have a home
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
                // ensure server/db filled if only ConnectionString provided
                tenant = FixupTenantFromConnStringIfNeeded(tenant);

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

        // When loading Tenants:Items from any json, also fill Server/Database if only ConnectionString was present
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
                        ConnectionString = el.TryGetProperty("ConnectionString", out var cs) ? cs.GetString() ?? "" : "",
                        Server = el.TryGetProperty("Server", out var sv) ? sv.GetString() ?? "" : "",
                        Database = el.TryGetProperty("Database", out var db) ? db.GetString() ?? "" : ""
                    };

                    t = FixupTenantFromConnStringIfNeeded(t);

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

        private static Tenant FixupTenantFromConnStringIfNeeded(Tenant t)
        {
            var server = t.Server;
            var db = t.Database;
            var name = t.Name;

            if ((string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(db)) &&
                !string.IsNullOrWhiteSpace(t.ConnectionString))
            {
                var (sv, dbParsed) = ParseServerAndDb(t.ConnectionString!);
                if (string.IsNullOrWhiteSpace(server)) server = sv;
                if (string.IsNullOrWhiteSpace(db)) db = dbParsed;
                if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(db)) name = db;
            }

            // If nothing changed, return the original instance
            if (server == t.Server && db == t.Database && name == t.Name)
                return t;

            // Otherwise return a new instance with the computed values
            return new Tenant
            {
                Id = t.Id,
                Name = name,
                ConnectionString = t.ConnectionString,
                Server = server,
                Database = db
            };
        }

        private static (string? server, string? database) ParseServerAndDb(string cs)
        {
            try
            {
                var b = new SqlConnectionStringBuilder(cs);
                var server = b.DataSource;
                var db = b.InitialCatalog;

                // normalize: ensure tcp: prefix unless an explicit scheme already exists
                if (!string.IsNullOrWhiteSpace(server) &&
                    !server.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase) &&
                    !server.StartsWith("np:", StringComparison.OrdinalIgnoreCase))
                {
                    server = "tcp:" + server;
                }

                return (server, string.IsNullOrWhiteSpace(db) ? null : db);
            }
            catch
            {
                // if it can't be parsed, leave nulls
                return (null, null);
            }
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
            => ReadConnectionStringFromFile(Path.Combine(contentRoot, "instance.settings.json"))
            ?? ReadConnectionStringFromFile(Path.Combine(contentRoot, "appsettings.json"));

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
