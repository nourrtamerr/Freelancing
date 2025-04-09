using Freelancing.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Milestone
{
    public int Id { get; set; }
	public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }


    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }


    public MilestoneStatus Status { get; set; }
    public  DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; } //duration in days
    public bool IsDeleted { get; set; } = false;
    public virtual MilestonePayment MilestonePayment { get; set; } //navigation property
}
public enum MilestoneStatus
{
    Pending,
    Completed
}