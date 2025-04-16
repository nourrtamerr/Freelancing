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
		public ActionResult<Freelancing.Models.Stripe.PaymentResponse> CreateCheckoutSession(string Amount, string redirectionurl)
		{

			try
			{
				var baseUrl = $"{Request.Scheme}://{Request.Host}";

				//var baseUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host;
				var currency = "usd"; // Currency code
									  //var successUrl = $"{Request.Scheme}://{Request.Host}/api/Order/PaymentSuccess?session_id={{CHECKOUT_SESSION_ID}}&successurl={{successurl}}";

				var successUrl = redirectionurl;
				var cancelUrl = $"{baseUrl}/api/Order/canceled";
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

			}
			catch (StripeException e)
			{
				_logger.LogError(e, "Stripe error");
				return BadRequest(new { error = e.StripeError.Message });
			}
		}

		[HttpGet("cancel")]
		[AllowAnonymous]
		public IActionResult Cancel(
										[FromQuery] string session_id,  // Stripe includes this automatically
										[FromQuery] string reason = "") // Optional: Stripe may provide cancellation reason
		{
			_logger.LogInformation($"Checkout canceled. Session: {session_id}, Reason: {reason}");

			// Optional: Fetch session details from Stripe
			var sessionService = new Stripe.Checkout.SessionService();
			var session = sessionService.Get(session_id);

			return Ok(new
			{
				Message = "Payment was canceled.",
				SessionId = session_id,
				Amount = session.AmountTotal / 100,
				Currency = session.Currency,
				CustomerEmail = session.CustomerDetails?.Email,
				// Frontend can use this to redirect or show UI
				RedirectUrl = "https://your-app.com/checkout/retry",
				reason = reason
			});
		}
	}
}
