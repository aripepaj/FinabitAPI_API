using AutoBit_WebInvoices.Models;
using FinabitAPI.Models;
using FinabitAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UsersRepository _dalUsers;
    private readonly IConfiguration _configuration;
    private readonly IAccountRepository _repo;

    public AccountController(UsersRepository dalUsers, IConfiguration configuration, IAccountRepository repo)
    {
        _dalUsers = dalUsers;
        _configuration = configuration;
        _repo = repo;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginBindingModel model)
    {
        var user = _dalUsers.GetLoginUser(model.Username, model.Password);
        if (user == null || user.ID <= 0)
            return Unauthorized();

        var claims = new[]
        {
        new Claim("UserID", user.ID.ToString())
    };

        var jwtConfig = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(48),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    // Example of a protected endpoint:
    [Authorize]
    [HttpGet("userinfo")]
    public IActionResult UserInfo()
    {
        var username = User.Identity.Name;
        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
        return Ok(new { username, userId });
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountMatchDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AccountMatchDto>>> Search(
            [FromQuery] string? accountId = null,
            [FromQuery] string? accountName = null,
            CancellationToken ct = default)
    {
        var data = await _repo.SearchAccountsAsync(accountId, accountName, ct);
        return Ok(data);
    }

    [HttpPost("searchBatch")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountProbeResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AccountProbeResult>>> SearchBatch(
    [FromBody] IReadOnlyList<AccountProbe> probes,
    CancellationToken ct = default)
    {
        if (probes == null || probes.Count == 0)
            return Ok(Array.Empty<AccountProbeResult>());

        const int maxDegreeOfParallelism = 8; 
        using var gate = new SemaphoreSlim(maxDegreeOfParallelism);

        var tasks = probes.Select(async p =>
        {
            await gate.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                var rows = await _repo.SearchAccountsAsync(p.AccountId, p.AccountName, ct).ConfigureAwait(false);
                return new AccountProbeResult
                {
                    Index = p.Index,
                    AccountId = p.AccountId,
                    AccountName = p.AccountName,
                    Results = rows
                };
            }
            catch
            {
                return new AccountProbeResult
                {
                    Index = p.Index,
                    AccountId = p.AccountId,
                    AccountName = p.AccountName,
                    Results = Array.Empty<AccountMatchDto>()
                };
            }
            finally
            {
                gate.Release();
            }
        });

        var arr = await Task.WhenAll(tasks).ConfigureAwait(false);

        var byIndex = arr.ToDictionary(x => x.Index);
        var ordered = probes.Select(p => byIndex[p.Index]).ToList();
        return Ok(ordered);
    }


    [HttpGet("getAll")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AccountListItemDto>>> GetAllAccounts(
            CancellationToken ct = default)
    {
        var rows = await _repo.GetAllAccountsAsync(ct);
        return Ok(rows);
    }

}
