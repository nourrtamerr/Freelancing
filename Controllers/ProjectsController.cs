using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [HttpGet("numberofclients")]
        [Authorize(Roles ="Freelancer")]
        public async Task<IActionResult> GetClientsNumber()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var projects = await context.GetAllProjectsAsync();
            var prs = projects.Where(p => p.FreelancerId == userId);
            var clients = projects.Where(p => p.FreelancerId == userId).Select(p => p.ClientId).Distinct().Count();
            return Ok(new  { clients, completed = prs.Where(p=>p.Status== projectStatus.Completed).Count(),
                //pending = prs.Where(p => p.Status == projectStatus.Pending).Count(),
                working = prs.Where(p => p.Status == projectStatus.Working).Count()
            });

        }

		//[HttpGet]
  //      [Authorize]
		//public async Task<ActionResult<List<Project>>> GetMyProjects()
		//{


        [HttpGet("id")]
        public async Task<ActionResult<List<Project>>> GetProjectsById(int id)
        {
            var project = await context.GetProjectDtoByIdAsync(id);
            return Ok(project);
        }
    }
}
