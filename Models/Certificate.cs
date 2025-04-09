using Freelancing.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Certificate
{
    public int Id { get; set; }
	public string Name { get; set; }
    public DateTime IssueDate { get; set; }
    [ForeignKey("Freelancer")]
    public string FreelancerId { get; set; }
	public Freelancer Freelancer { get; set; }
}