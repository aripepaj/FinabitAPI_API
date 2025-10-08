using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace FinabitAPI.Core.Server
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        private readonly ServerService _serverService;
        private readonly ILogger<ServerController> _logger;

        public ServerController(ServerService serverService, ILogger<ServerController> logger)
        {
            _serverService = serverService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the current server date and time
        /// </summary>
        /// <returns>Server date and time information</returns>
        [HttpGet("time")]
        public IActionResult GetServerTime()
        {
            try
            {
                _logger.LogInformation("Getting server time");
                
                var serverTime = _serverService.GetServerTime();
                
                _logger.LogInformation("Server time retrieved successfully: {ServerTime}", serverTime);
                return Ok(new { serverTime = serverTime });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting server time");
                return StatusCode(500, "Internal server error occurred while getting server time");
            }
        }

        /// <summary>
        /// Lists available SQL Server instances on the network
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <returns>List of available SQL Server instances</returns>
        [HttpGet("list")]
        public IActionResult ListServers([FromQuery] string connectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    _logger.LogWarning("Connection string is required");
                    return BadRequest("Connection string is required");
                }

                _logger.LogInformation("Listing available SQL Server instances");
                
                var servers = _serverService.ListServers(connectionString);
                
                if (servers != null && servers.Count > 0)
                {
                    _logger.LogInformation("Server list retrieved successfully with {Count} servers", servers.Count);
                    return Ok(servers);
                }
                else
                {
                    _logger.LogInformation("No servers found");
                    return Ok(new { message = "No SQL Server instances found on the network", servers = new List<object>() });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing SQL Server instances");
                return StatusCode(500, "Internal server error occurred while listing servers");
            }
        }
    }
}