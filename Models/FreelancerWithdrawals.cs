using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class FreelancerWithdrawals:Payment
	{
		[ForeignKey("Freelancer")]
		public int FreelancerId { set; get; }
		public Freelancer Freelancer { set; get; }
	}
}
