using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/list")]
    public class TodoProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("Hello World");
        }
    }
}
