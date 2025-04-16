﻿namespace Freelancing.DTOs.BiddingProjectDTOs
{
    public class BiddingProjectFilterDTO
    {
        //public List<int> projectType { get; set; }
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
        public List<int>? Currency { get; set; }
        //public DateOnly BidEndDate { get; set; }
        public List<int>? Category { get; set; }
        public List<int>? SubCategory { get; set; }
        public List<int>? Skills { get; set; }
        public List<int>? ExperienceLevel { get; set; }
        
        //public List<int> ExpectedDuration { get; set; } // 1-3months  3-6months  6-9months  fa momkn y5tar kza w7da mnhom
        
        public List<int> ClientCountry { get; set; }

        public int? MinExpectedDuration { get; set; }
        public int? MaxExpectedDuration { get; set; }

        public int? MinNumOfProposals { get; set; }
        public int? MaxNumOfProposals { get; set; }     
        

    }
}
