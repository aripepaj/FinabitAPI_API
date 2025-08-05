using AutoBit_WebInvoices.Models;
using FinabitAPI.Models;
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

    public AccountController(UsersRepository dalUsers, IConfiguration configuration)
    {
        _dalUsers = dalUsers;
        _configuration = configuration;
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
}
