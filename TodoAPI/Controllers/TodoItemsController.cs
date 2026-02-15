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

        [HttpGet("{id:guid}")]
        public IActionResult GetItem(Guid projectId, Guid id) 
        {
            var item = MemoryDataStore.Projects.SelectMany(p => p.Items).FirstOrDefault(i => i.Id == id);

            if (item is null) return NotFound();

            return Ok(item);
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

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid projectId, Guid id, [FromBody] TodoItem item)
        {
            var project = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project is null) return NotFound();

            var existingItem = project.Items.FirstOrDefault(i => i.Id == id);
            if (existingItem is null) return NotFound();
            if (string.IsNullOrWhiteSpace(item.Name)) return BadRequest("Item title is required.");

            var updatedItem = existingItem with
            {
                Name = item.Name,
                Description = item.Description,
                IsComplete = item.IsComplete,
                DueDate = item.DueDate,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var itemIndex = project.Items.IndexOf(existingItem);
            project.Items[itemIndex] = updatedItem;

            var projectIndex = MemoryDataStore.Projects.IndexOf(project);
            MemoryDataStore.Projects[projectIndex] = project with { UpdatedAt = DateTimeOffset.UtcNow };

            return Ok(updatedItem);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid projectId, Guid id)
        {
            var project = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project is null) return NotFound();

            var existingItem = project.Items.FirstOrDefault(i => i.Id == id);
            if (existingItem is null) return NotFound();

            project.Items.Remove(existingItem);

            var projectIndex = MemoryDataStore.Projects.IndexOf(project);
            MemoryDataStore.Projects[projectIndex] = project with { UpdatedAt = DateTimeOffset.UtcNow };

            return NoContent(); 
        }
    }
}
