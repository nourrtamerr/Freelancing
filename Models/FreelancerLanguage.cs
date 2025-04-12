using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class FreelancerLanguage
	{
		public int id { set; get; }
		public Language Language { get; set; }

		[ForeignKey("freelancer")]
		public string freelancerId { set; get; }
		public Freelancer freelancer { set; get; }
	}
}
