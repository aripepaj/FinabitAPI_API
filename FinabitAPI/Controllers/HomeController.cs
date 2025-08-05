using Microsoft.AspNetCore.Mvc;

namespace FinabitAPI.Controllers
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
