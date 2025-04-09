using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class Proposal
	{
		public int Id { get; set; }
		public string CoverLetter { get; set; }
		public decimal? Price { get; set; } // nullable for fixedpriceprojects

		[ForeignKey("Freelancer")]
		public string FreelancerId { get; set; }
		public virtual Freelancer Freelancer { get; set; }
		[ForeignKey("Project")]
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }

	}
}
