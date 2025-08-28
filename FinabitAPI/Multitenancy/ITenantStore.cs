namespace FinabitAPI.Multitenancy
{
    public interface ITenantStore
    {
        Task<Tenant?> FindAsync(string tenantId, CancellationToken ct = default);
        Task<Tenant?> GetDefaultAsync(CancellationToken ct = default);

        // NEW:
        Task<IReadOnlyCollection<Tenant>> GetAllAsync(CancellationToken ct = default);
        Task<bool> AddOrUpdateAsync(Tenant tenant, bool setAsDefault = false, CancellationToken ct = default);
        Task<bool> RemoveAsync(string tenantId, CancellationToken ct = default);
        Task<string?> GetDefaultIdAsync(CancellationToken ct = default);
    }
}
