using Microsoft.AspNetCore.Http;

namespace FinabitAPI.Multitenancy;

public sealed class HttpContextTenantAccessor : ITenantAccessor
{
    private readonly IHttpContextAccessor _http;
    private readonly ITenantStore _store;

    public HttpContextTenantAccessor(IHttpContextAccessor http, ITenantStore store)
    {
        _http = http;
        _store = store;
    }

    public Tenant Current
    {
        get
        {
            // Inside HTTP request and middleware set the tenant?
            var ctx = _http.HttpContext;
            if (ctx is not null && ctx.Items.TryGetValue("Tenant", out var t) && t is Tenant tenant)
                return tenant;

            // Outside a request or middleware didn't run yet -> fall back to default tenant
            var def = _store.GetDefaultAsync().GetAwaiter().GetResult();
            if (def is not null)
                return def;

            throw new InvalidOperationException(
                "No tenant resolved (and no default configured). " +
                "Ensure TenantResolutionMiddleware runs early or configure Tenants:DefaultId / ConnectionStrings:DefaultConnection.");
        }
    }
}
