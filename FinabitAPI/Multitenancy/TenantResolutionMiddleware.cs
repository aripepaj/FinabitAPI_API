using Microsoft.Extensions.Primitives;

namespace FinabitAPI.Multitenancy;

public sealed class TenantResolutionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx, ITenantStore store)
    {
        string? id = null;

        if (ctx.Request.Headers.TryGetValue("X-Tenant-Id", out var hdr) && !StringValues.IsNullOrEmpty(hdr))
            id = hdr.ToString();

        id ??= ctx.Request.Query["tenant"].FirstOrDefault();

        Tenant? tenant = null;

        if (!string.IsNullOrWhiteSpace(id))
        {
            tenant = await store.FindAsync(id, ctx.RequestAborted);
            // If provided id not found, fall back to default
            tenant ??= await store.GetDefaultAsync(ctx.RequestAborted);
        }
        else
        {
            tenant = await store.GetDefaultAsync(ctx.RequestAborted);
        }

        if (tenant is null)
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new { error = "No tenant resolved and no default configured in appsettings/tenants.json." });
            return;
        }

        ctx.Items["Tenant"] = tenant;
        await next(ctx);
    }
}
