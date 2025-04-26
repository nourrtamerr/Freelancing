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


        [HttpPost("Filter")]
        public async Task<IActionResult> GetAll([FromBody]BiddingProjectFilterDTO filters, [FromQuery] int pageNumber, [FromQuery] int PageSize)
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
            var project = await _biddingProjectService.CreateBiddingProjectAsync(p, "63d89bb1-7a13-4e02-bf19-14701398e3a1" /*User.FindFirstValue(ClaimTypes.NameIdentifier)*/);

            return Ok(new { project.ClientId, project.FreelancerId, project.Id });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]BiddingProjectCreateUpdateDTO p)
        {
            var project = await _biddingProjectService.UpdateBiddingProjectAsync(p, id);

            return Ok(new { project.ClientId, project.FreelancerId, project.Id });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _biddingProjectService.DeleteBiddingProjectAsync(id));
        }


        //[HttpPost("Filter")]
        //public async Task<IActionResult> Filter(BiddingProjectFilterDTO pdto, int PageNumber, int PageSize)
        //{
        //    return Ok(await _biddingProjectService.Filter(pdto, PageNumber, PageSize));
        //}
    }
}
