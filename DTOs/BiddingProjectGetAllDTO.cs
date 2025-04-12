namespace Freelancing.DTOs
{
    public class BiddingProjectGetAllDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectType { get; set; } = "Bidding";
        public int BidAveragePrice { get; set; } //average
        public int minimumPrice { get; set; }
        public int maximumprice { get; set; }
        public Currency currency { set; get; }
        public ExperienceLevel experienceLevel { get; set; }
        public List<string> ProjectSkills { get; set; } = new List<string>();
        public int? PostedFrom { get; set; }
        public int ClientTotalNumberOfReviews { get; set; }
        public double ClientRating { get; set; }

    }
}
