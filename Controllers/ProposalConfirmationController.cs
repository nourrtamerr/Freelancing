using AutoMapper;
using Freelancing.DTOs;
using Freelancing.Models;
using Humanizer;


////using Freelancing.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe.V2;
using System.Security.Claims;


namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalConfirmationController(INotificationRepositoryService _notification,IConfiguration configuration, IClientService clientService, IMapper mapper, ApplicationDbContext context) : ControllerBase
    {


        private string Pay(int proposalId, PaymentMethod method, string TransactionId)
        {
            var proposal = context.Proposals.Include(p=>p.suggestedMilestones).FirstOrDefault(p => p.Id == proposalId);
            var project = context.project.FirstOrDefault(p => p.Id == proposal.ProjectId);
            var Amount = proposal.suggestedMilestones.Sum(m => m.Amount);


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
                    TransactionId = TransactionId,
                    PaymentMethod = method,
                    ProposalId = proposalId,
                    Date = DateTime.Now
                });
                context.project.Update(project);
                context.SaveChanges();

				context.freelancers.FirstOrDefault(f => f.Id == (context.Proposals.Find(proposalId).FreelancerId)).RemainingNumberOfBids--;
				context.SaveChanges();
				var url = configuration["AppSettings:AngularAppUrl"] + "/Payments";

                return url;
            }
            return "Project not found";
        }





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

                    #region old

                    //var project = context.project.FirstOrDefault(p => p.Id == proposal.ProjectId);
                    //if (project is not null)
                    //{
                    //    project.FreelancerId = proposal.FreelancerId;
                    //    foreach (var milestone in proposal.suggestedMilestones)
                    //    {

                    //        project.Milestones.Add(new Milestone { Title = milestone.Description, Description = milestone.Description, Amount = milestone.Amount, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(milestone.Duration), ProjectId = project.Id, Status = MilestoneStatus.Pending });

                    //    }
                    //    context.ClientProposalPayments.Add(new()
                    //    {
                    //        Amount = Amount,
                    //        TransactionId = Guid.NewGuid().ToString(),
                    //        PaymentMethod = PaymentMethod.Balance,
                    //        ProposalId = proposalId,
                    //        Date = DateTime.Now
                    //    });
                    //    context.project.Update(project);
                    //    context.SaveChanges();
                    //    var url = configuration["AppSettings:AngularAppUrl"] + "/Payments";
                    //    return Redirect(url);
                    //}
                    #endregion

                    var url = Pay(proposalId, PaymentMethod.Balance, Guid.NewGuid().ToString());
                    return Ok();
                }
                return BadRequest("proposal not found");
            }
            return BadRequest("Client not found");
        }


        [HttpGet("ClientPayFromCard")]
        public async Task<IActionResult> ClientPayFromcard(int proposalId, [FromQuery]CardPaymentDTO card)
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
                   
                    var url = Pay(proposalId, PaymentMethod.CreditCard, card.Cardnumber + "," + card.cvv);
                    //return Redirect(url);
                    return Ok();
                }
                return BadRequest("proposal not found");
            }
            return BadRequest("Client not found");
        }







        [HttpGet("ClientPayFromStripe")]
        public async Task<IActionResult> ClientPayFromStripe(int proposalId)
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
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    var SuccessUrl = $"{baseUrl}/api/ProposalConfirmation/Success?session_id={{CHECKOUT_SESSION_ID}}&proposalId={proposalId}&";
                    var url = Url.ActionLink("CreateCheckoutSession", "Stripe", new { Amount = proposal.suggestedMilestones.Sum(m => m.Amount), redirectionurl = SuccessUrl });
                    return Redirect(url);

                    
                }
                return BadRequest("Proposal not found");
            }
            return BadRequest("Client not found");

        }




        [HttpGet("Success")]
        public async Task<IActionResult> Success(string session_id,int proposalId)
        {
			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			//var userId = Request.Cookies["UserSessionId"];
			var sessionService = new SessionService();
			var session = sessionService.Get(session_id, new SessionGetOptions
			{
				Expand = new List<string> { "line_items.data.price.product" }
			});
			if (!session.Metadata.TryGetValue("userId", out var userId))
			{
                // This userId is unique per session
                return BadRequest("payment failed");
			}
			var client = context.clients.FirstOrDefault(c => c.Id == userId);

            if(client is Client)
            {
                var proposal = context.Proposals.FirstOrDefault(p => p.Id == proposalId);
                if (proposal is not null)
                {
                    #region old
                    //var project = context.project.FirstOrDefault(p => p.Id == proposal.ProjectId);

                    //project.FreelancerId = proposal.FreelancerId;
                    //foreach (var milestone in proposal.suggestedMilestones)
                    //{
                    //    project.Milestones.Add(new Milestone { 
                    //        Title = milestone.Description,
                    //        Description = milestone.Description,
                    //        Amount = milestone.Amount, StartDate = DateTime.Now,
                    //        EndDate = DateTime.Now.AddDays(milestone.Duration),
                    //        ProjectId = project.Id,
                    //        Status = MilestoneStatus.Pending
                    //    });
                    //}

                    //var Amount = proposal.suggestedMilestones.Sum(m => m.Amount);

                    //context.ClientProposalPayments.Add(new()
                    //{
                    //    Amount = Amount,
                    //    TransactionId = Guid.NewGuid().ToString(),
                    //    PaymentMethod = PaymentMethod.Stripe,
                    //    ProposalId = proposalId,
                    //    Date = DateTime.Now
                    //});
                    //context.project.Update(project);
                    //context.SaveChanges();
                    //var url = configuration["AppSettings:AngularAppUrl"] + "/Payments";
                    //return Redirect(url); 
                    #endregion


                    var url = Pay(proposalId, PaymentMethod.Stripe, session_id);
                   
					await _notification.CreateNotificationAsync(new()
					{
						isRead = false,
						Message = $"Payment successful for proposal Proposal with id `{proposalId}` please check it",
						UserId = userId
					});
					await _notification.CreateNotificationAsync(new()
					{
						isRead = false,
						Message = $"Your proposal with id `{proposalId}` has been confirmed, please check your projects at {configuration["AppSettings:AngularAppUrl"]}/myProjects",
						UserId = proposal.FreelancerId
					});
					return Redirect(url);
                }
                return BadRequest("Proposal not found");
            }
            return BadRequest("Client not found");


        }


    }



}
