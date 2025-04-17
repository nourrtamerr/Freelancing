using Freelancing.DTOs;
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
		[HttpPost("WithdrawCard")]
		[Authorize]
		public async Task<IActionResult> WithdrawCard(CardPaymentDTO dto)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if(userid is null)
			{
				return BadRequest("user not found");
					
			}
			var user = await _freelancers.FindByIdAsync(userid);
			if (user is Freelancer freelancer1)
			{
				freelancer1.Balance = freelancer1.Balance + dto.amount;
				Withdrawal withdrawal = new Withdrawal()
				{
					Amount = dto.amount,
					Date = DateTime.Now,
					FreelancerId = userid,
					TransactionId = dto.Cardnumber+","+dto.cvv,
					PaymentMethod = PaymentMethod.Stripe
				};
				_context.Withdrawals.Add(withdrawal);
				await _freelancers.UpdateAsync(freelancer1);
				_context.SaveChanges();
				var url = configuration["AppSettings:AngularAppUrl"] + "/PaymentSuccess";
				return Redirect(url);

			}
			if (user is Client client1)
			{

				client1.Balance = client1.Balance - dto.amount;
				Withdrawal withdrawal = new Withdrawal()
				{
					Amount = dto.amount,
					Date = DateTime.Now,
					ClientId = userid,
					TransactionId = dto.Cardnumber + "," + dto.cvv,
					PaymentMethod = PaymentMethod.Stripe
				};
				_context.Withdrawals.Add(withdrawal);
				await _freelancers.UpdateAsync(client1);
				_context.SaveChanges();
				var url = configuration["AppSettings:AngularAppUrl"] + "/PaymentSuccess";
				return Redirect(url);
			}
			return BadRequest("not a correct client");
		}
		[HttpPost("AddFundsCard")]
		[Authorize]
		public async Task<IActionResult> AddFundsCard(CardPaymentDTO dto)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userid is null)
			{
				return BadRequest("user not found");

			}
			var user = await _freelancers.FindByIdAsync(userid);
			if (user is Freelancer freelancer1)
			{
				freelancer1.Balance = freelancer1.Balance + dto.amount;
				AddingFunds FUND = new AddingFunds()
				{
					Amount = dto.amount,
					Date = DateTime.Now,
					FreelancerId = userid,
					TransactionId = dto.Cardnumber + "," + dto.cvv,
					PaymentMethod = PaymentMethod.Stripe
				};
				_context.Funds.Add(FUND);
				await _freelancers.UpdateAsync(freelancer1);
				_context.SaveChanges();
				var url = configuration["AppSettings:AngularAppUrl"] + "/PaymentSuccess";
				return Redirect(url);

			}
			if (user is Client client1)
			{

				client1.Balance = client1.Balance + dto.amount;
				AddingFunds FUND = new AddingFunds()
				{
					Amount = dto.amount,
					Date = DateTime.Now,
					ClientId = userid,
					TransactionId = dto.Cardnumber + "," + dto.cvv,
					PaymentMethod = PaymentMethod.Stripe
				};
				_context.Funds.Add(FUND);
				await _freelancers.UpdateAsync(client1);
				_context.SaveChanges();
				var url = configuration["AppSettings:AngularAppUrl"] + "/PaymentSuccess";
				return Redirect(url);
			}
			return BadRequest("not a correct client");
		}
		#region stripe
		[HttpGet("AddFundsStripe")]
		[Authorize]
		public async Task<IActionResult> StripeAddFunds(int money, string email)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userid == null)
			{
				return Unauthorized();
			}
			var freelancer = await _freelancers.FindByIdAsync(userid);
			if (freelancer == null)
			{
				return BadRequest("user not found");

			}
			if (freelancer is Freelancer freelancer1)
			{


			}
			if (freelancer is Client client1)
			{


			}

			var baseUrl = $"{Request.Scheme}://{Request.Host}";
			var successUrl = $"{baseUrl}/api/FreelancerWithdrawal/fundssuccess?session_id={{CHECKOUT_SESSION_ID}}&amount={money}";
			var url = Url.ActionLink("CreateCheckoutSession", "Stripe", new { Amount = money.ToString(), redirectionurl = successUrl });
			return Redirect(url);

		}


		[HttpGet("fundssuccess")]
		public async Task<IActionResult> fundsSuccess(string session_id, string amount)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var freelancer = await _freelancers.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

			if (freelancer is Freelancer current)
			{

				decimal x;
				bool ret = decimal.TryParse(amount, out x);
				if (ret)
				{
					current.Balance = current.Balance + x;
					Withdrawal withdrawal = new Withdrawal()
					{
						Amount = x,
						Date = DateTime.Now,
						FreelancerId = userid,
						TransactionId = session_id,
						PaymentMethod = PaymentMethod.Stripe
					};
					_context.Withdrawals.Add(withdrawal);
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
			else if (freelancer is Client current2)
			{
				decimal x;
				bool ret = decimal.TryParse(amount, out x);
				if (ret)
				{
					current2.Balance = current2.Balance + x;
					Withdrawal withdrawal = new Withdrawal()
					{
						Amount = x,
						Date = DateTime.Now,
						ClientId = userid,
						TransactionId = session_id,
						PaymentMethod = PaymentMethod.Stripe
					};
					_context.Withdrawals.Add(withdrawal);
					await _freelancers.UpdateAsync(current2);
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


		[HttpGet("StripeWithdraw")]
		[Authorize]
		public async Task<IActionResult> StripeWithdraw(int money, string email)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userid == null)
			{
				return Unauthorized();
			}
			var freelancer = await _freelancers.FindByIdAsync(userid);
			if (freelancer == null)
			{
				return BadRequest("user not found");

			}
			if (freelancer is Freelancer freelancer1)
			{
				if (freelancer1.Balance < money)
				{
					return BadRequest("Not enough balance");
				}

			}
			if (freelancer is Client client1)
			{
				if (client1.Balance < money)
				{
					return BadRequest("Not enough balance");
				}

			}

			#region withdrawal
			else
			{
				return BadRequest("you are not a freelancer");
			}

			if (freelancer is Freelancer freelancerr && string.IsNullOrEmpty(freelancerr.StripeAccountId))
			{
				// Redirect to connect account setup
				var url2 = Url.ActionLink("CreateConnectedAccount", "Stripe", new { freelancerEmail = email, amountInCents = (long)money * 1000 });
				return Redirect(url2);
			}
			if (freelancer is Client clientt && string.IsNullOrEmpty(clientt.StripeAccountId))
			{
				// Redirect to connect account setup
				var url2 = Url.ActionLink("CreateConnectedAccount", "Stripe", new { freelancerEmail = email, amountInCents = (long)money * 1000 });
				return Redirect(url2);
			}
			var id = (freelancer is Freelancer freelancereno) ? freelancereno.StripeAccountId : (freelancer as Client).StripeAccountId;
			var url3 = Url.ActionLink("TransferToFreelancer", "Stripe", new { connectedAccountId = id, amountInCents = (long)money * 1000 });
			//return RedirectToAction("CreateConnectedAccount", "Stripe", new { freelancerEmail = email, amountInCents = (long)money * 1000 });

			return Redirect(url3);
			#endregion



		}



		[HttpGet("Success")]
		public async Task<IActionResult> Success(string session_id, string amount)
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
					Withdrawal withdrawal = new Withdrawal()
					{
						Amount = x,
						Date = DateTime.Now,
						FreelancerId = userid,
						TransactionId = session_id,
						PaymentMethod = PaymentMethod.Stripe
					};
					_context.Withdrawals.Add(withdrawal);
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
			else if (freelancer is Client current2)
			{
				decimal x;
				bool ret = decimal.TryParse(amount, out x);
				if (ret)
				{
					current2.Balance = current2.Balance - x;
					Withdrawal withdrawal = new Withdrawal()
					{
						Amount = x,
						Date = DateTime.Now,
						ClientId = userid,
						TransactionId = session_id,
						PaymentMethod = PaymentMethod.Stripe
					};
					_context.Withdrawals.Add(withdrawal);
					await _freelancers.UpdateAsync(current2);
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
		#endregion



	}
}
