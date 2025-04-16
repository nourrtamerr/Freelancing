using Freelancing.DTOs.MilestoneDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : ControllerBase
    {
        private readonly IMilestoneService _milestoneService;

        public MilestoneController(IMilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _milestoneService.GetAllAsync());
        }



        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _milestoneService.GetByIdAsync(id));
        }


        [HttpGet("Project/{ProjectId}")]
        public async Task<IActionResult> GetByProjectId(int ProjectId)
        {
            return Ok(await _milestoneService.GetByProjectId(ProjectId));
        }

      



        [HttpPost]
        public async Task<IActionResult> Post(MilestoneCreateDTO milestoneDto)
        {
            return Ok(await _milestoneService.CreateAsync(milestoneDto));
        }




        
        [HttpPatch("UpdateStatus/{MilestoneId}")]
        public async Task<IActionResult> Patch(int MilestoneId, [FromBody]int StatusId)
        {
            return Ok(await _milestoneService.UpdateStatusAsync(MilestoneId, StatusId));
        }


        [HttpPut]
        public async Task<IActionResult> Put(MilestoneGetByIdOrProjectIdDTO milestone)
        {
            return Ok(await _milestoneService.UpdateAsync(milestone));
        }




       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _milestoneService.DeleteAsync(id));
        }
    }
}
