using Freelancing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.V2;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerWithdrawalController(IConfiguration configuration,ApplicationDbContext _context,UserManager<AppUser> _freelancers) : ControllerBase
    {
		[HttpGet("StripeWithdraw")]
		[Authorize(Roles ="Freelancer")]
		public async Task<IActionResult> StripeWithdraw(int money,string email)
		{
			if(((await _freelancers.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier))) as Freelancer )
				.Balance <= money)
			{
				return BadRequest("Not enough balance");
			}
			var baseUrl = $"{Request.Scheme}://{Request.Host}";
			var successUrl = $"{baseUrl}/api/FreelancerWithdrawal/success?session_id={{CHECKOUT_SESSION_ID}}&amount={money}";
			var url = Url.ActionLink("CreateCheckoutSession", "Stripe", new { Amount = money.ToString(), redirectionurl = successUrl });
			url = Url.ActionLink("CreateConnectedAccount", "Stripe", new { freelancerEmail =email });
			
			return Redirect(url);
		}



		[HttpGet("Success")]
		public async Task<IActionResult> Success(string session_id,string amount)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var freelancer = await _freelancers.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (freelancer is Freelancer current)
			{
				decimal x;
				bool ret = decimal.TryParse(amount, out x);
				if (ret) 
				{ 
				current.Balance = current.Balance - x;
				FreelancerWithdrawals withdrawal = new FreelancerWithdrawals()
				{
					Amount = x,
					Date = DateTime.Now,
					FreelancerId = userid,
					TransactionId = session_id,
					PaymentMethod = PaymentMethod.Stripe

				};
				_context.FreelancerWithdrawals.Add(withdrawal);
				await _freelancers.UpdateAsync(current);
				_context.SaveChanges();
					var url = configuration["AppSettings:AngularAppUrl"] + "/PaymentSuccess";
					return Redirect(url);
				}
					else
					{
						return BadRequest("incorrect amount");
					}
			}
			else
			{
				return Unauthorized();
			}
			
		}
	}
}
