using Microsoft.Extensions.Configuration;

namespace FinabitAPI.Core.Multitenancy;

public sealed class FileTenantStore : ITenantStore
{
    private readonly Dictionary<string, Tenant> _tenants;
    private readonly Tenant? _default;
    private readonly string? _defaultId;

    public FileTenantStore(IConfiguration cfg)
    {
        var list = cfg.GetSection("Tenants:Items").Get<List<Tenant>>() ?? new();
        _tenants = list.ToDictionary(t => t.Id, StringComparer.OrdinalIgnoreCase);

        var defaultId = cfg.GetValue<string>("Tenants:DefaultId");
        if (defaultId is not null && _tenants.TryGetValue(defaultId, out var d))
        {
            _default = d;
            _defaultId = defaultId;
        }

        // Final fallback to appsettings connection string
        if (_default is null && cfg.GetConnectionString("DefaultConnection") is string cs)
        {
            _default = new Tenant
            {
                Id = "appsettings",
                Name = "AppSettings Default",
                ConnectionString = cs
            };
            _tenants[_default.Id] = _default;
            _defaultId = _default.Id;
        }
    }

    public Task<Tenant?> FindAsync(string tenantId, CancellationToken ct = default)
    {
        if (_tenants.TryGetValue(tenantId, out var t))
            return Task.FromResult<Tenant?>(t);

        return GetDefaultAsync(ct);
    }

    public Task<Tenant?> GetDefaultAsync(CancellationToken ct = default) =>
        Task.FromResult(_default);

    // NEW: satisfy extended interface
    public Task<IReadOnlyCollection<Tenant>> GetAllAsync(CancellationToken ct = default) =>
        Task.FromResult<IReadOnlyCollection<Tenant>>(_tenants.Values.OrderBy(t => t.Id).ToArray());

    public Task<string?> GetDefaultIdAsync(CancellationToken ct = default) =>
        Task.FromResult(_defaultId);

    public Task<bool> AddOrUpdateAsync(Tenant tenant, bool setAsDefault = false, CancellationToken ct = default) =>
        throw new NotSupportedException("FileTenantStore is read-only. Register MutableFileTenantStore to enable CRUD.");

    public Task<bool> RemoveAsync(string tenantId, CancellationToken ct = default) =>
        throw new NotSupportedException("FileTenantStore is read-only. Register MutableFileTenantStore to enable CRUD.");
}
