using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Core.Global
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { Title = "Home Page", Message = "Welcome to the API Home!" });
        }
    }
}
