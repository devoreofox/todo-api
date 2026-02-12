using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/list")]
    public class TodoProjectsController : ControllerBase
    {

        private static readonly List<TodoProject> _projects = new();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_projects);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoProject project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
            {
                return BadRequest("Project name is required.");
            }

            var newProject = new TodoProject
            {
                Id = Guid.NewGuid(),
                Name = project.Name,
                Description = project.Description ?? string.Empty,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            _projects.Add(newProject);

            return Created(nameof(Get), newProject);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody] TodoProject project)
        {

            var existingProject = _projects.FirstOrDefault(p => p.Id == id);
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
                Items = project.Items,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            var index = _projects.IndexOf(existingProject);
            _projects[index] = updatedProject;
            return Ok(updatedProject);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existingProject = _projects.FirstOrDefault(p => p.Id == id);
            if (existingProject is null)
            {
                return NotFound();
            }
            _projects.Remove(existingProject);
            return NoContent();
        }
    }
}
