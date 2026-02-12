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

            return CreatedAtAction(nameof(Get), newProject);
        }
    }
}
