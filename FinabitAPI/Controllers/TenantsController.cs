using FinabitAPI.Multitenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
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

        var (okConn, err) = await TryOpenAsync(cs, timeoutSeconds: 5, ct);
        if (!okConn) return BadRequest($"Failed to connect to the database with the provided server/database. {err}");

        var t = new Tenant
        {
            Id = dto.Id,
            Name = dto.Name,
            Server = dto.Server,
            Database = dto.Database,
            ConnectionString = cs              
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

        var (okConn, err) = await TryOpenAsync(cs, timeoutSeconds: 5, ct);
        if (!okConn) return BadRequest($"Failed to connect to the database with the provided server/database. {err}");

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

    private static async Task<(bool ok, string? error)> TryOpenAsync(string connectionString, int timeoutSeconds, CancellationToken ct)
    {
        try
        {
            var sb = new SqlConnectionStringBuilder(connectionString)
            {
                ConnectTimeout = timeoutSeconds
            };

            await using var cn = new SqlConnection(sb.ConnectionString);
            await cn.OpenAsync(ct);

            await using var cmd = new SqlCommand("SELECT 1;", cn)
            {
                CommandType = CommandType.Text,
                CommandTimeout = timeoutSeconds
            };
            _ = await cmd.ExecuteScalarAsync(ct);

            return (true, null);
        }
        catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException || ex is TimeoutException)
        {
            return (false, ex.Message);
        }
    }

}
