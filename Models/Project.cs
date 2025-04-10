using Freelancing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

public abstract class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
	public DateTime EndDate { get; set; } // in days
	public Currency currency { set; get; }
	

	[ForeignKey("Client")]
    public string ClientId { get; set; }
    public virtual AppUser Client { get; set; }
	[ForeignKey("Freelancer")]
    public string? FreelancerId { get; set; }
	public virtual AppUser? Freelancer { get; set; }
	
	[NotMapped]
    public projectStatus Status =>  FreelancerId==null? projectStatus.Pending 
		: this.Milestones.All(m=>m.Status==MilestoneStatus.Completed) ? projectStatus.Completed:
		projectStatus.Working;
	[ForeignKey("Subcategory")]
    public int SubcategoryId { get; set; }
	public virtual Subcategory Subcategory { set; get; }
	public virtual List<Milestone> Milestones { get; set; } = new List<Milestone>();
	public ExperienceLevel experienceLevel { get; set; }
	public virtual List<Proposal> Proposals { get; set; } = new List<Proposal>();
}


public enum ExperienceLevel
{
	Entry,
	Intermediate,
	Expert
}
public enum projectStatus
{
	Pending,
	Working,
	Completed
}
public enum Currency
{
	USD,
	EUR,
	GBP,
	JPY,
	AUD,
	CAD,
	CHF,
	CNY,
	SEK,
	NZD
}