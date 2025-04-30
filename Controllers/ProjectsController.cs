using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace Freelancing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController(IProjectService context, UserManager<AppUser> userManager) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<List<Project>>> GetAllProjects()
		{
			var projects = await context.GetAllProjectsDtoAsync();
			return Ok(projects);

		}


		[HttpGet("MyProjects")]
		public async Task<ActionResult<List<Project>>> MyProjects()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var user = await userManager.FindByIdAsync(userId);
			var projects = await context.GetAllProjectsDtoAsync();
			if (user is Client)
			{
				projects = projects.Where(p => p.ClientId == userId).ToList();

			}
			else if (user is Freelancer)
			{
				projects = projects.Where(p => p.FreelancerId == userId).ToList();

			}
			else
			{
				return BadRequest(new { message = "Admins dont have projects" });
			}

			return Ok(projects);

		}

		[HttpGet("numberofclients")]
		[Authorize(Roles = "Freelancer")]
		public async Task<IActionResult> GetClientsNumber()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			//var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var projects = await context.GetAllProjectsAsync();
			var prs = projects.Where(p => p.FreelancerId == userId);
			var clients = projects.Where(p => p.FreelancerId == userId).Select(p => p.ClientId).Distinct().Count();
			return Ok(new
			{
				clients,
				completed = prs.Where(p => p.Status == projectStatus.Completed).Count(),
				//pending = prs.Where(p => p.Status == projectStatus.Pending).Count(),
				working = prs.Where(p => p.Status == projectStatus.Working).Count()
			});

		}





		[HttpGet("{id}")]
		public async Task<ActionResult<List<Project>>> GetProjectsById(int id)
		{
			var project = await context.GetProjectDtoByIdAsync(id);
			return Ok(project);
		}
	}
}