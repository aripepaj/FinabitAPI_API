using FinabitAPI.Core.User.dto;
using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Core.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user with username and password
        /// </summary>
        /// <param name="loginRequest">Login credentials</param>
        /// <returns>User information if authentication successful</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (
                    loginRequest == null
                    || string.IsNullOrEmpty(loginRequest.UserName)
                    || string.IsNullOrEmpty(loginRequest.Password)
                )
                {
                    _logger.LogWarning("Invalid login request - missing username or password");
                    return BadRequest("Username and password are required");
                }

                _logger.LogInformation(
                    "Processing login request for username: {UserName}",
                    loginRequest.UserName
                );

                var user = _usersService.GetLoginUser(loginRequest.UserName, loginRequest.Password);

                if (user != null && user.ID > 0 && user.HasConnections && user.Status)
                {
                    _logger.LogInformation(
                        "Login successful for username: {UserName}",
                        loginRequest.UserName
                    );

                    // Return user info without sensitive data
                    return Ok(
                        new
                        {
                            userId = user.ID,
                            userName = user.UserName,
                            departmentId = user.DepartmentID,
                            roleId = user.RoleID,
                            partnerId = user.PartnerID,
                            defaultPartnerName = user.DefaultPartnerName,
                            isAuthoriser = user.IsAuthoriser,
                            expireDate = user.ExpireDate,
                            status = user.Status,
                            message = "Login successful",
                        }
                    );
                }
                else
                {
                    _logger.LogWarning(
                        "Login failed for username: {UserName}",
                        loginRequest.UserName
                    );
                    return Unauthorized("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error during login for username: {UserName}",
                    loginRequest?.UserName
                );
                return StatusCode(500, "Internal server error occurred during login");
            }
        }

        /// <summary>
        /// Validates user credentials
        /// </summary>
        /// <param name="loginRequest">Login credentials</param>
        /// <returns>Validation result</returns>
        [HttpPost("validate")]
        public IActionResult ValidateUser([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (
                    loginRequest == null
                    || string.IsNullOrEmpty(loginRequest.UserName)
                    || string.IsNullOrEmpty(loginRequest.Password)
                )
                {
                    _logger.LogWarning("Invalid validation request - missing username or password");
                    return BadRequest("Username and password are required");
                }

                _logger.LogInformation(
                    "Validating user credentials for username: {UserName}",
                    loginRequest.UserName
                );

                bool isValid = _usersService.ValidateUser(
                    loginRequest.UserName,
                    loginRequest.Password
                );

                return Ok(
                    new
                    {
                        isValid = isValid,
                        message = isValid ? "User is valid" : "Invalid credentials",
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error during user validation for username: {UserName}",
                    loginRequest?.UserName
                );
                return StatusCode(500, "Internal server error occurred during validation");
            }
        }

        /// <summary>
        /// Gets user information by user ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User information</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                _logger.LogInformation("Getting user by ID: {UserID}", id);

                var user = _usersService.GetUserById(id);

                if (user != null && user.ID > 0)
                {
                    return Ok(
                        new
                        {
                            userId = user.ID,
                            userName = user.UserName,
                            departmentId = user.DepartmentID,
                            roleId = user.RoleID,
                            partnerId = user.PartnerID,
                            defaultPartnerName = user.DefaultPartnerName,
                            isAuthoriser = user.IsAuthoriser,
                            expireDate = user.ExpireDate,
                            status = user.Status,
                        }
                    );
                }
                else
                {
                    _logger.LogWarning("User not found with ID: {UserID}", id);
                    return NotFound($"User with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by ID: {UserID}", id);
                return StatusCode(500, "Internal server error occurred while getting user");
            }
        }

        /// <summary>
        /// Gets user information by username
        /// </summary>
        /// <param name="username">The username to search</param>
        /// <returns>User information if found</returns>
        [HttpGet("by-username/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    _logger.LogWarning("Username is required for GetUserByUsername");
                    return BadRequest("Username is required");
                }

                _logger.LogInformation("Getting user by username: {UserName}", username);

                var user = _usersService.GetUserByUsername(username);

                if (user != null && user.ID > 0)
                {
                    return Ok(
                        new
                        {
                            userId = user.ID,
                            userName = user.UserName,
                            departmentId = user.DepartmentID,
                            roleId = user.RoleID,
                            partnerId = user.PartnerID,
                            defaultPartnerName = user.DefaultPartnerName,
                            isAuthoriser = user.IsAuthoriser,
                            expireDate = user.ExpireDate,
                            status = user.Status,
                        }
                    );
                }

                _logger.LogWarning("User not found with username: {UserName}", username);
                return NotFound($"User with username '{username}' not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username: {UserName}", username);
                return StatusCode(500, "Internal server error occurred while getting user");
            }
        }

        /// <summary>
        /// Checks if a user is active
        /// </summary>
        /// <param name="userName">Username to check</param>
        /// <returns>User status</returns>
        [HttpGet("status/{userName}")]
        public IActionResult CheckUserStatus(string userName)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    _logger.LogWarning("Username is required for status check");
                    return BadRequest("Username is required");
                }

                _logger.LogInformation("Checking status for username: {UserName}", userName);

                bool isActive = _usersService.CheckUserStatus(userName);

                return Ok(
                    new
                    {
                        userName = userName,
                        isActive = isActive,
                        message = isActive ? "User is active" : "User is not active",
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error checking user status for username: {UserName}",
                    userName
                );
                return StatusCode(500, "Internal server error occurred while checking user status");
            }
        }
    }

    // DTO for login requests
    public class LoginRequest
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
