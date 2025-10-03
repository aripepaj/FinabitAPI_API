using FinabitAPI.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public sealed class BasicAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly UsersRepository _userRepository;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        UsersRepository userRepository)
        : base(options, logger, encoder, clock)
    {
        _userRepository = userRepository;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string username, password;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.NoResult();

            var raw = authHeader.Parameter ?? string.Empty;
            var credentialBytes = Convert.FromBase64String(raw);
            var parts = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            if (parts.Length != 2) return AuthenticateResult.Fail("Invalid Basic header.");
            username = parts[0];
            password = parts[1];
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization header.");
        }

        var user = _userRepository.GetLoginUser(username, password);
        if (user == null || user.ID == 0)
            return AuthenticateResult.Fail("Invalid username or password.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Name, username)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.Headers["WWW-Authenticate"] = "Basic realm=\"FinabitAPI\"";
        return base.HandleChallengeAsync(properties);
    }
}
