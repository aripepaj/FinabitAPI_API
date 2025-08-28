using Microsoft.AspNetCore.Http;

namespace FinabitAPI.Multitenancy;

public sealed class Tenant
{
    public string Id { get; init; } = default!;
    public string? Name { get; init; }
    public string? ConnectionString { get; init; }
    public string? Server { get; init; }
    public string? Database { get; init; }
}

public interface ITenantAccessor
{
    Tenant Current { get; }
}