using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/list")]
    public class TodoProjectsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var projects = new List<TodoProject>
            {
                new TodoProject
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 1",
                    Description = "Description for project 1",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                },
                new TodoProject
                {
                    Id = Guid.NewGuid(),
                    Name = "Project 2",
                    Description = "Description for project 2",
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };

            return Ok(projects);
        }
    }
}
