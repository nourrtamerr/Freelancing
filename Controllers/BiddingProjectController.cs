using Freelancing.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<IActionResult> Create(BiddingProjectCreateDTO p)
        {
            return Ok(await _biddingProjectService.CreateBiddingProjectAsync(p));
        }

    }
}
