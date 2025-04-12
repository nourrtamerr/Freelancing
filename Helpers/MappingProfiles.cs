using AutoMapper;
using Freelancing.DTOs;

namespace Freelancing.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Milestone, MilestoneDTO>();
            CreateMap<MilestoneDTO, Milestone>();

            CreateMap<BiddingProjectDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectDTO>();

            CreateMap<BiddingProjectGetAllDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectGetAllDTO>();


        }
    }
}
