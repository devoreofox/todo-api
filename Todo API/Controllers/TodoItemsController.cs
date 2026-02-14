using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Data;

namespace TodoAPI.Controllers
{
    [Route("api/list/{projectId:guid}/items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetItems(Guid projectId)
        {
            var project = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project is null) return NotFound();

            return Ok(project.Items);
        }
    }
}
