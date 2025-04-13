namespace Freelancing.DTOs
{
    public class BiddingProjectCreateDTO
    {
        public string ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Currency currency { set; get; }

        public int minimumPrice { get; set; }
        public int maximumprice { get; set; }

        public DateTime BiddingStartDate { get; set; }
        public DateTime BiddingEndDate { get; set; }

        public List<string> projectSkills { get; set; }
        public string ExperienceLevel { get; set; }

        public int ExpectedDuration { get; set; }
        public string SubCategoryName { get; set; }
    }
}
