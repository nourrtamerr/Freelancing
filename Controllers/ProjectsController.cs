using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            var projects = await context.GetAllProjectsDtoAsync();
            return Ok(projects);

        }

        [HttpGet("id")]
        public async Task<ActionResult<List<Project>>> GetProjectsById(int id)
        {
            var project = await context.GetProjectDtoByIdAsync(id);
            return Ok(project);
        }
    }
}
