﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Freelancing.Models;
using Stripe;
using static Freelancing.Models.Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Stripe.V2;
namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController(ApplicationDbContext _context,StripeSettings _stripesettings, ILogger<StripeController> _logger, IConfiguration configuration) : ControllerBase
    {
		// method(DTO) 
		// var baseUrl = $"{Request.Scheme}://{Request.Host}";
		// var successUrl = $"{baseUrl}/api/Order/success?session_id={{CHECKOUT_SESSION_ID}}";
		//MilestonePayment
		//success(string session_id)
		//Redirect()
		/*
		 * 
		 * confirmproposalcreditcard(proposalid, creditcardinfo)
		 * {
		 * 
		 * }
		ConfirmProposal(ProposalId){
		var successUrl = $"{baseUrl}/api/Proposal/success?session_id={{CHECKOUT_SESSION_ID}}&proposalid={proposal.id}";
		return RedirectToAction("CreateCheckoutSession", "Stripe", new { Amount = proposal.price,redirectionurl=successUrl  });
		}
		success(string session_id,int proposalid)
		{

		redirect (_configuration["AppSettings:AngularAppUrl"]/PaymentSuccess)
		}






		ConfirmSubscriptionPlan(subscriptionplanid)
		{
			var successUrl = $"{baseUrl}/api/SubscriptionPlansPayment/success?session_id={{CHECKOUT_SESSION_ID}}&subscriptionplanid={subscriptionplan.id}";
			return RedirectToAction("CreateCheckoutSession", "Stripe", new { Amount = subscriptionplan.price, redirectionurl=successUrl });
		}

		stripe


		success(subscriptionplanid)
		{
		User.findfirstvalue
		Redirect (_configuration["AppSettings:AngularAppUrl"]/PaymentSuccess)
		}
		 */






		[HttpGet("create-checkout-session")]
		public ActionResult<Freelancing.Models.Stripe.PaymentResponse> CreateCheckoutSession(string Amount, string redirectionurl,bool payout=false)
		{

			try
			{
				var baseUrl = $"{Request.Scheme}://{Request.Host}";

				//var baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;
				var currency = "usd"; // Currency code
									  //var successUrl = $"{Request.Scheme}://{Request.Host}/api/Order/PaymentSuccess?session_id={{CHECKOUT_SESSION_ID}}&successurl={{successurl}}";

				var successUrl = redirectionurl;
				var cancelUrl = $"{baseUrl}/api/Stripe/cancel";
				StripeConfiguration.ApiKey = _stripesettings.SecretKey;

				var options = new SessionCreateOptions
				{
					PaymentMethodTypes = new List<string>
					{
						"card"
					},
					LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							Currency = currency,
							UnitAmount = Convert.ToInt32(Math.Ceiling(decimal.Parse(Amount))) * 100,  // Amount in smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = "Product Name",
								Description = "Product Description"
							}
						},
						Quantity = 1
					}
				},
					Mode = "payment",
					SuccessUrl = successUrl,
					CancelUrl = cancelUrl
				};

				var service = new Stripe.Checkout.SessionService();
				var session = service.Create(options);
				//var successUrl = $"{baseUrl}/Invoice/Create/{session.Id}";
				//$"{Request.Scheme}://{Request.Host}/api/Order/PaymentSuccess?session_id={{session.Id}}&successurl={{successurl}}"
				options.SuccessUrl = redirectionurl;
				session.SuccessUrl = redirectionurl;


				
				return Ok(new { session.Url });
				//return Redirect( session.Url);

			}
			catch (StripeException e)
			{
				_logger.LogError(e, "Stripe error");
				return BadRequest(new { error = e.StripeError.Message });
			}
		}
		
		
		


		
		[Authorize]
		[HttpGet("create-connected-account")]
		public IActionResult CreateConnectedAccount(string freelancerEmail,long amountInCents)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var freelancer = _context.freelancers.FirstOrDefault(f => f.Id == userId);
			var client = _context.clients.FirstOrDefault(f => f.Id == userId);
			if (freelancer == null&&client==null)
				return NotFound("Freelancer not found.");

			StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			var account = new AccountService().Create(new AccountCreateOptions
			{
				Type = "express",
				Country = "US",
				Email = freelancer?.Email??client.Email,
				Capabilities = new AccountCapabilitiesOptions
				{
					Transfers = new AccountCapabilitiesTransfersOptions { Requested = true }
				}
			});
			if(freelancer is not null)
			freelancer.StripeAccountId = account.Id;
			else
			{
				client.StripeAccountId = account.Id;
			}
				_context.SaveChanges();

			var link = new AccountLinkService().Create(new AccountLinkCreateOptions
			{
				Account = account.Id,
				RefreshUrl = $"{Request.Scheme}://{Request.Host}/api/stripe/create-connected-account?amountInCents={amountInCents}",
				ReturnUrl = $"{Request.Scheme}://{Request.Host}/api/stripe/onboarding-complete?accountId={account.Id}&amountInCents={amountInCents}",
				Type = "account_onboarding"
			});
			return Ok(new { link.Url });

			//return Redirect(link.Url);
		}
		
		
		



		
		[HttpGet("onboard-freelancer")]
		public IActionResult OnboardFreelancer(string accountId)
		{
			StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			var linkOptions = new AccountLinkCreateOptions
			{
				Account = accountId,
				RefreshUrl = $"{Request.Scheme}://{Request.Host}/api/stripe/onboard-freelancer?accountId={accountId}",
				ReturnUrl = $"{Request.Scheme}://{Request.Host}/onboarding-complete",
				Type = "account_onboarding"
			};

			var linkService = new AccountLinkService();
			var accountLink = linkService.Create(linkOptions);
			var url = Url.ActionLink("onboard-freelancer", "Stripe", new { connectedAccountId = accountId, amountInCents=8000 });
			return Ok(new { url = accountLink.Url });
		}

		
		



		
		[HttpGet("onboarding-complete")]
		public IActionResult OnboardingComplete(string accountId, long amountInCents)
		{
			// User completed onboarding — now do the transfer
			return RedirectToAction(nameof(TransferToFreelancer), new { connectedAccountId = accountId, amountInCents });
		}

		
		
		



		
		[Authorize]
		[HttpGet("transfer-to-freelancer")]
		public IActionResult TransferToFreelancer(string connectedAccountId, long amountInCents)
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var freelancer = _context.freelancers.FirstOrDefault(f => f.Id == userid);
			var client = _context.clients.FirstOrDefault(f => f.Id == userid);

			if (freelancer == null || string.IsNullOrEmpty(freelancer.StripeAccountId))
				if(client == null || string.IsNullOrEmpty(client.StripeAccountId))
				return BadRequest(" not onboarded yet.");

			StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			var options = new PaymentIntentCreateOptions
			{
				Amount = amountInCents,
				Currency = "usd",
				PaymentMethodTypes = new List<string> { "card" },
				TransferData = new PaymentIntentTransferDataOptions
				{
					Destination = freelancer?.StripeAccountId??client.StripeAccountId
				},
				ApplicationFeeAmount = amountInCents*(long)0.2,
			};

			var service = new PaymentIntentService();
			var intent = service.Create(options);



			//StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			//var account = new AccountService().Get(connectedAccountId);
			//if (!account.PayoutsEnabled)
			//	return BadRequest("Account not ready for payouts.");

			////var transfer = new TransferService().Create(new TransferCreateOptions
			////{
			////	Amount = amountInCents,
			////	Currency = "usd",
			////	Destination = connectedAccountId,
			////	Description = "Freelancer payout"
			////});
			////fake test mode 

			//string session_id= Guid.NewGuid().ToString();
			var url = Url.Action("Success", "FreelancerWithdrawal", new { session_id = intent.ClientSecret, amount = amountInCents / 1000 });
			//// Redirect to frontend success page

			return Redirect(url);
		}






		[HttpGet("cancel")]
		[AllowAnonymous]
		public IActionResult Cancel()
		{
			return Redirect(configuration["AppSettings:AngularAppUrl"]+"/PaymentCancelled");
		}
	}
}
