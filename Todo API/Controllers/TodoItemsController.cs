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


        [HttpPost]
        public IActionResult Post(Guid projectId, [FromBody] TodoItem item)
        {
            var project = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == projectId);

            if (project is null) return NotFound();

            if (string.IsNullOrWhiteSpace(item.Name)) return BadRequest("Item title is required.");

            var newItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                IsComplete = false,
                DueDate = item.DueDate,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var index = MemoryDataStore.Projects.IndexOf(project);
            MemoryDataStore.Projects[index] = project with { UpdatedAt = DateTimeOffset.UtcNow };

            project.Items.Add(newItem);

            return CreatedAtAction(nameof(GetItems), new { projectId }, newItem);
        }
    }
}
