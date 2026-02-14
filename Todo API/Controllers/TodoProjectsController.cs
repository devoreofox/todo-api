using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models;
using static TodoAPI.Data.MemoryDataStore;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/list")]
    public class TodoProjectsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProjects()
        {
            return Ok(MemoryDataStore.Projects);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProject(Guid id)
        {
            var project = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == id);
            if (project is null) return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoProject project)
        {
            if (string.IsNullOrWhiteSpace(project.Name)) return BadRequest("Project name is required.");

            var newProject = new TodoProject
            {
                Id = Guid.NewGuid(),
                Name = project.Name,
                Description = project.Description ?? string.Empty,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            MemoryDataStore.Projects.Add(newProject);

            return CreatedAtAction(nameof(GetProject), new { id = newProject.Id }, newProject);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody] TodoProject project)
        {

            var existingProject = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == id);
            if (existingProject is null)
            {
                return NotFound();
            }
            if (string.IsNullOrWhiteSpace(project.Name))
            {
                return BadRequest("Project name is required.");
            }

            var updatedProject = existingProject with
            {
                Name = project.Name,
                Description = project.Description ?? string.Empty,
                Items = existingProject.Items,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var index = MemoryDataStore.Projects.IndexOf(existingProject);
            MemoryDataStore.Projects[index] = updatedProject;
            return Ok(updatedProject);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existingProject = MemoryDataStore.Projects.FirstOrDefault(p => p.Id == id);
            if (existingProject is null)
            {
                return NotFound();
            }
            MemoryDataStore.Projects.Remove(existingProject);
            return NoContent();
        }
    }
}
