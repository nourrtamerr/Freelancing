using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Freelancing.Models;
using Stripe;
using static Freelancing.Models.Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Authorization;
namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController(StripeSettings _stripesettings, ILogger<StripeController> _logger, IConfiguration configuration) : ControllerBase
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
		[HttpGet("create-connected-account")]
		public IActionResult CreateConnectedAccount(string freelancerEmail)
		{
			StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			var accountOptions = new AccountCreateOptions
			{
				Type = "express", // Or "custom" if you want full control
				Country = "US",
				Email = freelancerEmail,
				Capabilities = new AccountCapabilitiesOptions
				{
					Transfers = new AccountCapabilitiesTransfersOptions
					{
						Requested = true
					}
				}
			};

			var accountService = new AccountService();
			var account = accountService.Create(accountOptions);

			// Save this account.Id to your database linked with the freelancer
			var url= Url.ActionLink("onboard-freelancer", "Stripe", new { accountId=account.Id });
			return Redirect(url);
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

		[HttpGet("transfer-to-freelancer")]
		public IActionResult TransferToFreelancer(string connectedAccountId, long amountInCents)
		{
			StripeConfiguration.ApiKey = _stripesettings.SecretKey;

			var accountService = new AccountService();
			var account = accountService.Get(connectedAccountId);  // Make sure the account is ready and capable of receiving payouts

			if (account.PayoutsEnabled)
			{
				var transferService = new TransferService();
				var transfer = transferService.Create(new TransferCreateOptions
				{
					Amount = amountInCents, // e.g., 8000 = $80
					Currency = "usd",
					Destination = connectedAccountId,
					Description = "Freelancer payment"
				});

				return Ok(new { transferId = transfer.Id });
			}
			else
			{
				return BadRequest("Account is not fully onboarded or payouts are not enabled.");
			}
		}
		[HttpGet("cancel")]
		[AllowAnonymous]
		public IActionResult Cancel()
		{
			return Redirect(configuration["AppSettings:AngularAppUrl"]+"/PaymentCancelled");
		}
	}
}
