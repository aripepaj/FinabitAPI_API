using Microsoft.Data.SqlClient;
using FinabitAPI.Multitenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantsController : ControllerBase
{
    private readonly ITenantStore _store;

    public TenantsController(ITenantStore store) => _store = store;

    // ---- helpers ----
    private static string BuildConnString(string server, string database)
    {
        // ensure tcp:
        if (!server.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase) &&
            !server.StartsWith("np:", StringComparison.OrdinalIgnoreCase))
            server = "tcp:" + server;

        var sb = new SqlConnectionStringBuilder
        {
            DataSource = server,          // e.g. tcp:192.168.199.30,1433
            InitialCatalog = database,
            UserID = "Fina",
            Password = "Fina-10",
            Encrypt = true,
            TrustServerCertificate = true,
            PersistSecurityInfo = false,
            ConnectTimeout = 15
        };
        sb["ConnectRetryCount"] = 3;
        sb["ConnectRetryInterval"] = 2;
        sb["Pooling"] = true;
        sb["Min Pool Size"] = 1;

        return sb.ConnectionString;
    }

    // Only show safe fields in responses
    private static object Shape(Tenant t) => new { t.Id, t.Name, t.Server, t.Database };

    [HttpGet]
    public async Task<ActionResult<object>> GetAll(CancellationToken ct)
    {
        var items = (await _store.GetAllAsync(ct)).Select(Shape).ToArray();
        var def = await _store.GetDefaultIdAsync(ct);
        return Ok(new { defaultId = def, items });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetById(string id, CancellationToken ct)
    {
        var t = await _store.FindAsync(id, ct);
        return t is null ? NotFound() : Ok(Shape(t));
    }

    public sealed class UpsertTenantDto
    {
        public string Id { get; set; } = default!;
        public string? Name { get; set; }
        public string Server { get; set; } = default!;
        public string Database { get; set; } = default!;
        public bool SetAsDefault { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertTenantDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Id)) return BadRequest("Id is required.");
        if (string.IsNullOrWhiteSpace(dto.Server) || string.IsNullOrWhiteSpace(dto.Database))
            return BadRequest("Server and Database are required.");

        var cs = BuildConnString(dto.Server, dto.Database);
        var t = new Tenant
        {
            Id = dto.Id,
            Name = dto.Name,
            Server = dto.Server,
            Database = dto.Database,
            ConnectionString = cs              // <- persist full CS to tenants.json
        };

        var ok = await _store.AddOrUpdateAsync(t, dto.SetAsDefault, ct);
        return ok ? CreatedAtAction(nameof(GetById), new { id = t.Id }, Shape(t)) : Problem("Failed to save tenant.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpsertTenantDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id is required.");
        if (string.IsNullOrWhiteSpace(dto.Server) || string.IsNullOrWhiteSpace(dto.Database))
            return BadRequest("Server and Database are required.");

        var cs = BuildConnString(dto.Server, dto.Database);
        var t = new Tenant
        {
            Id = id,
            Name = dto.Name,
            Server = dto.Server,
            Database = dto.Database,
            ConnectionString = cs
        };

        var ok = await _store.AddOrUpdateAsync(t, dto.SetAsDefault, ct);
        return ok ? NoContent() : Problem("Failed to update tenant.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
        => (await _store.RemoveAsync(id, ct)) ? NoContent() : NotFound();
}
