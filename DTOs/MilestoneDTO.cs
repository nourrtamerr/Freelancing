namespace Freelancing.DTOs
{
    public class MilestoneDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int? ProjectId { get; set; }
        public MilestoneStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } //duration in days
    }
}
