using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Freelancing.Models;
using static Freelancing.Models.Stripe;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Stripe;
using System.Collections.Generic;
using Freelancing.Helpers;
using Stripe.Checkout;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionPlanPaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StripeSettings _stripeSettings;
        private readonly ILogger<SubscribtionPlanPaymentController> _logger;

        public SubscribtionPlanPaymentController(ApplicationDbContext context, IOptions<StripeSettings> stripeSettings, ILogger<SubscribtionPlanPaymentController> logger)
        {
            _context = context;
            _stripeSettings = stripeSettings.Value;
            _logger = logger;
        }

        [HttpGet("create-checkout-session")]
        public async Task<ActionResult> CreateCheckoutSession(int planId, string redirectionurl)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null) return Unauthorized("User not authenticated.");

                // Check if user is a Freelancer or Client
                var freelancer = await _context.freelancers.FirstOrDefaultAsync(f => f.Id == userId);
                var client = await _context.clients.FirstOrDefaultAsync(c => c.Id == userId);

                if (freelancer == null && client == null)
                {
                    return BadRequest("User must be a freelancer or client to subscribe.");
                }

                // Get the plan
                var plan = SubscriptionPlansHelper.SubscriptionPlans.FirstOrDefault(p => p.Id == planId);
                if (plan == null)
                {
                    return BadRequest("Invalid subscription plan.");
                }

                // If it's a free plan, activate it immediately for both freelancers and clients
                if (plan.Price == 0)
                {
                    if (freelancer != null)
                    {
                        freelancer.subscriptionPlanId = plan.Id;
                        freelancer.RemainingNumberOfBids = plan.TotalNumber;
                    
                    }
                    else if (client != null)
                    {
                        client.subscriptionPlanId = plan.Id;
                    }

                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Free plan activated successfully." });
                }



                // var url = $"{Request.Scheme}://{Request.Host}/Stripe/create-checkout-session?Amount={plan.Price}";
                var PaymentSuccessUrl = Url.ActionLink("PaymentSuccess", "SubscribtionPlanPayment", new
                {
                    sessionId = "{CHECKOUT_SESSION_ID}",
                    planid = plan.Id

                });
                var url = Url.ActionLink("CreateCheckoutSession", "Stripe", new
                {
                    Amount = plan.Price,
                    redirectionurl = PaymentSuccessUrl,
                });


            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error");
                return BadRequest(new { error = ex.StripeError.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "An error occurred while creating the payment session." });
            }

            return Ok();
        }
        [HttpGet("payment-success")]
        public IActionResult PaymentSuccess(string sessionId, int planid)
        {
            try
            {
                var sessionService = new SessionService();
                var session = sessionService.Get(sessionId);
                if (session == null)
                {
                    return NotFound("Session not found.");
                }
                // Handle successful payment here (e.g., update user subscription status)
                // You can also redirect to the success URL if needed
                return Ok(new { message = "Payment successful.", session });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during payment success handling");
                return StatusCode(500, new { error = "An error occurred while processing the payment." });
            }
        }
    }
}
