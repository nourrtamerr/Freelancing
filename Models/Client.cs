using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class Client:AppUser
	{
		public bool PaymentVerified { get; set; } = false;


        [ForeignKey("subscriptionPlan")]
        public int? subscriptionPlanId { get; set; } = 2;
        public virtual SubscriptionPlan? subscriptionPlan { get; set; }


        public int? RemainingNumberOfProjects { get; set; } = 6;

    }
}
