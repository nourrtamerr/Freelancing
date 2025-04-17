using AutoMapper;
using Freelancing.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalConfirmationController(IClientService clientService, IMapper mapper, ApplicationDbContext context) : ControllerBase
    {


        [HttpGet("ClientPayFromBalance")]
        public async Task<IActionResult> ClientPayFromBalance(decimal Amount, int proposalId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var client = await context.clients.FirstOrDefaultAsync(c => c.Id == userId);
            if (client == null)
            {
                return BadRequest("user not found");
            }

            if(client is Client C)
            {
                if (C.Balance < Amount)
                {
                    return BadRequest("Not enough balance");
                }
                else
                {
                    C.Balance -= Amount;
                    var proposal = context.Proposals.FirstOrDefault(p => p.Id == proposalId);                    
                    
                    if (proposal is not null)
                    {
                        var project = context.project.FirstOrDefault(p => p.Id == proposal.ProjectId);
                        if (project is not null)
                        {
                            project.FreelancerId = proposal.FreelancerId;
                            foreach(var milestone in proposal.suggestedMilestones)
                            {
                                
                                project.Milestones.Add(new Milestone { Title=milestone.Title, Description=milestone.Description, Amount=milestone.Amount, StartDate=DateTime.Now, EndDate= DateTime.Now.AddDays(milestone.Duration), ProjectId=project.Id, Status=MilestoneStatus.Pending});
                                
                            }
                            return Ok(C);
                        }
                    }
                    return BadRequest("Proposal not found");
                }
            }
            return BadRequest("Client not found");

        }


    }
}
