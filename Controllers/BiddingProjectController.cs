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
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _biddingProjectService.GetAllBiddingProjectsAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _biddingProjectService.GetBiddingProjectByIdAsync(id));
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(BiddingProjectCreateUpdateDTO p)
        //{
        //    var project = await _biddingProjectService.CreateBiddingProjectAsync(p, User.FindFirstValue(ClaimTypes.NameIdentifier));

        //    return Ok(new {project.ClientId,project.Subcategory.Name,p=project.ProjectSkills.Select(ps=>ps.Skill.Name).ToList()});
        //}

        [HttpPost]
        public async Task<IActionResult> Create(BiddingProjectCreateUpdateDTO p)
        {
            try
            {
                var project = await _biddingProjectService.CreateBiddingProjectAsync(p, User.FindFirstValue(ClaimTypes.NameIdentifier));

                //return Ok(new
                //{
                //    project.ClientId,
                //    project.Subcategory.Name,
                //    p = project.ProjectSkills.Select(ps => ps.Skill.Name).ToList()
                //});
                return Ok(new
                {
                    ClientId = project.ClientId ?? "3611f18e-2097-4b01-bcb3-0fcf8045af03",
                    project.Subcategory.Name,
                    p = project.ProjectSkills.Select(ps => ps.Skill.Name).ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, details = ex.StackTrace });
            }
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


        [HttpPost("Filter")]
        public async Task<IActionResult> Filter(BiddingProjectFilterDTO pdto, int PageNumber, int PageSize)
        {
            return Ok(await _biddingProjectService.Filter(pdto,PageNumber, PageSize));
        }
    }
}
