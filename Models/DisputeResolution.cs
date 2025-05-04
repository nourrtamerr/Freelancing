namespace Freelancing.Models
{
	public class DisputeResolution
	{
		public int id { set; get; }
		public int MilestoneId { set; get; }
		public Milestone milestone { set; get; }
		public string Complaint { set; get; }
	}
}
