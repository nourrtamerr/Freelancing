using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService context,UserManager<AppUser> _users) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            var projects = await context.GetAllProjectsAsync();
            return Ok(projects);

        }
		//[HttpGet]
  //      [Authorize]
		//public async Task<ActionResult<List<Project>>> GetMyProjects()
		//{

		//	var projects = await context.GetAllProjectsAsync();
  //          var user = await _users.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
  //          if(user is Client)
  //          {
                
  //          }
		//	return Ok(projects);

		//}
	}
}
