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

    [HttpGet]
    public async Task<ActionResult<object>> GetAll(CancellationToken ct)
    {
        var items = await _store.GetAllAsync(ct);
        var def = await _store.GetDefaultIdAsync(ct);
        return Ok(new { defaultId = def, items });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tenant>> GetById(string id, CancellationToken ct)
    {
        var t = await _store.FindAsync(id, ct);
        return t is null ? NotFound() : Ok(t);
    }

    public sealed class UpsertTenantDto
    {
        public string Id { get; set; } = default!;
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }
        public string? Server { get; set; }
        public string? Database { get; set; }
        public bool SetAsDefault { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UpsertTenantDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Id))
            return BadRequest("Id is required.");

        var t = new Tenant
        {
            Id = dto.Id,
            Name = dto.Name,
            ConnectionString = dto.ConnectionString,
            Server = dto.Server,
            Database = dto.Database
        };

        var ok = await _store.AddOrUpdateAsync(t, dto.SetAsDefault, ct);
        if (!ok) return Problem("Failed to save tenant.");
        return CreatedAtAction(nameof(GetById), new { id = t.Id }, t);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpsertTenantDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id is required.");
        dto.Id = id;
        var t = new Tenant
        {
            Id = dto.Id,
            Name = dto.Name,
            ConnectionString = dto.ConnectionString,
            Server = dto.Server,
            Database = dto.Database
        };
        var ok = await _store.AddOrUpdateAsync(t, dto.SetAsDefault, ct);
        return ok ? NoContent() : Problem("Failed to update tenant.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var ok = await _store.RemoveAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
