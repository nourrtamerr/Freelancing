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
            var projects = await context.GetAllProjectsAsync();
            return Ok(projects);

        }
		[HttpGet]
		public async Task<ActionResult<List<Project>>> GetMyProjects()
		{
			var projects = await context.GetAllProjectsAsync();
			return Ok(projects);

		}
	}
}
