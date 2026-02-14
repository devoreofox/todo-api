using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [Route("api/list/{projectId:guid}/items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok("Hello World");
        }
    }
}
