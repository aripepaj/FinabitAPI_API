using AutoBit_WebInvoices.Models;
using FinabitAPI.Models;
using FinabitAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly AccountDetailsRepository _accountDetailsRepo;

    public ReportsController(AccountDetailsRepository accountDetailsRepo)
    {
        _accountDetailsRepo = accountDetailsRepo;
    }

    [HttpGet("account-details")]
    public async Task<ActionResult<List<AccountDetail>>> GetAccountDetails(
    [FromQuery] DateTime from,
    [FromQuery] DateTime to,
    [FromQuery] string accountPattern = "%",
    CancellationToken ct = default)
    {
        if (to < from) return BadRequest("Parameter 'to' must be >= 'from'.");

        try
        {
            var data = await _accountDetailsRepo.GetAccountDetailsAsync(from, to, accountPattern, ct);
            return Ok(data);
        }
        catch (Exception ex)
        {
            // TODO: inject ILogger<ReportsController> and log ex
            return Problem(title: "Account details failure", detail: ex.Message, statusCode: 500);
        }
    }
}