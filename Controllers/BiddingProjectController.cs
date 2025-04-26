using Freelancing.DTOs.AuthDTOs;
using Freelancing.DTOs.BiddingProjectDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiddingProjectController : ControllerBase
    {
        private readonly IBiddingProjectService _biddingProjectService;

        public BiddingProjectController(IBiddingProjectService biddingProjectService)
        {
            _biddingProjectService = biddingProjectService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]BiddingProjectFilterDTO filters, [FromQuery] int pageNumber, [FromQuery] int PageSize)
        {

            return Ok(await _biddingProjectService.GetAllBiddingProjectsAsync(filters, pageNumber, PageSize));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _biddingProjectService.GetBiddingProjectByIdAsync(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(BiddingProjectCreateUpdateDTO p)
        {
            var project = await _biddingProjectService.CreateBiddingProjectAsync(p, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(new {project.ClientId,project.Subcategory.Name,p=project.ProjectSkills.Select(ps=>ps.Skill.Name).ToList()});
        }


        [HttpPut("/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]BiddingProjectCreateUpdateDTO p)
        {
            var project = await _biddingProjectService.UpdateBiddingProjectAsync(p, id);

            return Ok(new { project.ClientId, project.Subcategory.Name, p = project.ProjectSkills.Select(ps => ps.Skill.Name).ToList() });
        }


        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _biddingProjectService.DeleteBiddingProjectAsync(id));
        }

        [HttpGet("GetMyBiddingProjects")]
        public async Task<IActionResult> GetMyBiddingProjectsAll([FromQuery] int pageNumber, [FromQuery] int PageSize)
        {
            userRole role;
            if (User.GetType() == typeof(Client))
            {
                role = userRole.Client;
            }
            else if (User.GetType() == typeof(Freelancer))
            {
                role = userRole.Freelancer;
            }
            else
            {
                throw new Exception("User is not a client or freelancer");
            }

            return Ok(await _biddingProjectService.GetmyBiddingProjectsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), role, pageNumber, PageSize));
        }



        //[HttpPost("Filter")]
        //public async Task<IActionResult> Filter(BiddingProjectFilterDTO pdto, int PageNumber, int PageSize)
        //{
        //    return Ok(await _biddingProjectService.Filter(pdto,PageNumber, PageSize));
        //}
    }
}
