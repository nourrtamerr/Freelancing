using AutoMapper;
using Freelancing.DTOs.ProposalDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController(IMapper _mapper,IproposalService _proposals,IProjectService _projects) : ControllerBase
    {
        // GET: api/<ProposalController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProposalController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProposalController>
        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> Post([FromBody]CreateProposalDTO dto)
        {
            //var proposal = _mapper.Map<Proposal>(dto);
            var project = await _projects.GetProjectByIdAsync(dto.ProjectId);

			if (project is null)
            {
				return BadRequest("Project not found");
			}
            if(project.FreelancerId is not null)
            {
                return BadRequest("Project is already assigned to a freelancer");
            }
            dto.type=project.GetType()==typeof(FixedPriceProject)? projectType.fixedprice : projectType.bidding;
            if (dto.type == projectType.fixedprice)
            {
                if((project as FixedPriceProject).fixedPrice!=dto.Price)
                {
                    return BadRequest("not matching the price");
                }
            }
			await _proposals.CreateProposalAsync(dto, User.FindFirstValue(ClaimTypes.NameIdentifier));
			return Ok();
        }

        // PUT api/<ProposalController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<ProposalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
