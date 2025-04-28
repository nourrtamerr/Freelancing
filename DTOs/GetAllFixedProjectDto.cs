namespace Freelancing.DTOs
{
    public class GetAllFixedProjectDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; } //price for the whole project
        public string Description { get; set; }

        public Currency Currency { get; set; }

        public int ExpectedDuration { get; set; }

        public int SubcategoryID { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }
        public int ProposalsCount { get; set; }
        public List<string> ProjectSkills { get; set; } = new List<string>();
        public List<MilestoneDto> Milestones { get; set; } = new List<MilestoneDto>();

        public decimal Price { get; set; }



    }
}
