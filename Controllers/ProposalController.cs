using AutoMapper;
using Freelancing.DTOs.ProposalDTOS;
using Freelancing.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController(UserManager<AppUser> usermanager,IMapper _mapper,IproposalService _proposals,IProjectService _projects) : ControllerBase
    {
        // GET: api/<ProposalController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			return Ok(await _proposals.GetAllProposalsAsync());
		}

        // GET api/<ProposalController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
			return Ok(await (_proposals.GetProposalByIdAsync(id)));
        }

		[HttpGet("getbyprojectId/{id}")]
		public async Task<IActionResult> Getbyprojectid(int id)
		{
			return Ok(await (_proposals.GetProposalsByProjectIdAsync(id)));
		}

		[HttpGet("getbyfreelancerId/{name}")]
		public async Task<IActionResult> Getbyfreelancerid(string name)
		{
			var user =await usermanager.FindByNameAsync(name);
			if(user is null)
			{
				return BadRequest("usr not found");
			}
			
				return Ok(await (_proposals.GetProposalsByFreelancerIdAsync(user.Id)));
		}
		
		[HttpGet("getbyfreelancerId")]
		public async Task<IActionResult> GetProposalsbyfreelancerid()
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return BadRequest("usr not found");
            }
            return Ok(await (_proposals.GetProposalsByFreelancerIdAsync(userId)));
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
                //if((project as FixedPriceProject).fixedPrice!=dto.Price)
                //{
                //    return BadRequest("not matching the price");
                //}
            }
            else
            {
                if(project is BiddingProject prjct)
				{
					if (prjct.minimumPrice > dto.Price || prjct.maximumprice <dto.Price)
					{
						return BadRequest("not within bid limits");
					}
					if (prjct.BiddingEndDate < DateTime.Now )
					{
						return BadRequest("bid is over");
					}
				}
			}
			return Ok(
				await _proposals.CreateProposalAsync(dto, User.FindFirstValue(ClaimTypes.NameIdentifier))
				);
        }

        // PUT api/<ProposalController>/5
        [HttpPut("{id}")]
		[Authorize(Roles = "Freelancer")]
		[ServiceFilter(typeof(AuthorFilter))]
		public async Task<IActionResult> Update(int id,[FromBody]EditProposalDTO dto)
        {

			var project = await _projects.GetProjectByIdAsync((await _proposals.GetProposalByIdAsync(id)).ProjectId);

			if (project is null)
			{
				return BadRequest("Project not found");
			}
			if (project.FreelancerId is not null)
			{
				return BadRequest("Project is already assigned to a freelancer");
			}
			dto.type = project.GetType() == typeof(FixedPriceProject) ? projectType.fixedprice : projectType.bidding;
			if (dto.type == projectType.fixedprice)
			{
				if ((project as FixedPriceProject).Price != dto.Price)
				{
					return BadRequest("not matching the price");
				}
			}
			else
			{
				if (project is BiddingProject prjct)
				{
					if (prjct.minimumPrice > dto.Price || prjct.maximumprice < dto.Price)
					{
						return BadRequest("not within bid limits");
					}
					if (prjct.BiddingEndDate < DateTime.Now)
					{
						return BadRequest("bid is over");
					}
				}
			}

			return Ok(await _proposals.UpdateProposalAsync(id,dto));
		}

        // DELETE api/<ProposalController>/5
        [HttpDelete("{id}")]
		[Authorize]
		[ServiceFilter(typeof(AuthorAndAdminFilter))]
		public async Task<IActionResult> Delete(int id)
        {
			await _proposals.DeleteProposalAsync(id);
			return Ok(new { Message = "Deleted" });
		}


    }
}
