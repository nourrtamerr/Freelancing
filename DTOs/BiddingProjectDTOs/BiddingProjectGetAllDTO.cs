﻿namespace Freelancing.DTOs.BiddingProjectDTOs
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
        public string currency { set; get; }
        public string experienceLevel { get; set; }
        public List<string> ProjectSkills { get; set; } = new List<string>();
        public int? PostedFrom { get; set; }
        public int ClientTotalNumberOfReviews { get; set; }
        public double ClientRating { get; set; }
        public string ClientCountry { get; set; }
        public int NumOfBids { get; set; }
        public bool ClientIsVerified { get; set; }
        public int ExpectedDuration { get; set; }
        public DateTime BiddingStartDate { get; set; }
        public DateTime BiddingEndDate { get; set; }

    }
}
