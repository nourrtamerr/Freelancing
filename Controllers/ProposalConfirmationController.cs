using AutoMapper;
////using Freelancing.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalConfirmationController(IConfiguration configuration, IClientService clientService, IMapper mapper, ApplicationDbContext context) : ControllerBase
    {


        [HttpGet("ClientPayFromBalance")]
        public async Task<IActionResult> ClientPayFromBalance(int proposalId)
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

            if (client is Client C)
            {

                var proposal = context.Proposals.Include(p => p.suggestedMilestones).FirstOrDefault(p => p.Id == proposalId);

                if (proposal is not null)
                {
                    var Amount = proposal.suggestedMilestones.Sum(m => m.Amount);
                    if (C.Balance < Amount)
                    {
                        return BadRequest("Not enough balance");
                    }
                    C.Balance -= Amount;

                    var project = context.project.FirstOrDefault(p => p.Id == proposal.ProjectId);
                    if (project is not null)
                    {
                        project.FreelancerId = proposal.FreelancerId;
                        foreach (var milestone in proposal.suggestedMilestones)
                        {

                            project.Milestones.Add(new Milestone { Title = milestone.Description, Description = milestone.Description, Amount = milestone.Amount, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(milestone.Duration), ProjectId = project.Id, Status = MilestoneStatus.Pending });

                        }
                        context.ClientProposalPayments.Add(new()
                        {
                            Amount = Amount,
                            TransactionId = Guid.NewGuid().ToString(),
                            PaymentMethod = PaymentMethod.Balance,
                            ProposalId = proposalId,
                            Date = DateTime.Now
                        });
                        context.project.Update(project);
                        context.SaveChanges();
						var url = configuration["AppSettings:AngularAppUrl"] + "/Payments";
						return Redirect(url);
					}
                }
                return BadRequest("Freelancer not found");
            }
            return BadRequest("Client not found");
        }
    
    }


    
}
