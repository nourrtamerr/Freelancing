using AutoMapper;
using Freelancing.DTOs;
using Freelancing.DTOs.AuthDTOs;

namespace Freelancing.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Milestone, MilestoneDTO>();
            CreateMap<MilestoneDTO, Milestone>();
            #region AuthMappings
            CreateMap<RegisterDTO, Client>()
               .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<Client, RegisterDTO>();
            CreateMap<Freelancer, RegisterDTO>();
            CreateMap<RegisterDTO, Freelancer>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<CreateAdminDTO, Admin>()
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<EditProfileDTO, AppUser>()
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<AppUser, UsersRequestingVerificationViewDTO>();
            CreateMap<RegisterDTO, Client>()
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<Client, RegisterDTO>();
            CreateMap<Freelancer, RegisterDTO>();
            CreateMap<RegisterDTO, Freelancer>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<CreateAdminDTO, Admin>()
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<EditProfileDTO, AppUser>()
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());
            CreateMap<AppUser, UsersRequestingVerificationViewDTO>();
            CreateMap<Freelancer, ViewFreelancersDTO>()
                .ForMember(dest => dest.UserSkills, opt =>
                opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).ToList()));

            //CreateMap<Review, FreelancerReviewDTO>()
            //.ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer != null ? src.Reviewer.UserName : string.Empty));

            CreateMap<Freelancer, ViewFreelancerPageDTO>()
               .ForMember(dest => dest.UserSkills, opt =>
               opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).ToList()))
               .ForMember(dest => dest.Languages, opt =>
               opt.MapFrom(src => src.Languages.Select(us => us.Language.ToString()).ToList()));


            #endregion

            CreateMap<BiddingProjectDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectDTO>();


            CreateMap<BiddingProjectGetAllDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectGetAllDTO>()
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => "Bidding"))
                .ForMember(dest => dest.ProjectSkills, opt => opt.MapFrom(src => src.ProjectSkills.Select(ps => ps.Skill.Name).ToList()))
                .ForMember(dest => dest.BidAveragePrice, opt => opt.MapFrom(src =>
                    src.Proposals != null && src.Proposals.Any()
                    ? (int)src.Proposals.Average(p => p.Price)
                    : 0
                ))
                .ForMember(dest => dest.PostedFrom, opt => opt.MapFrom(src =>
                    (DateTime.Now - src.BiddingStartDate).Days
                ))
                .ForMember(dest => dest.ClientRating, opt => opt.Ignore()) // to be set manually
                .ForMember(dest => dest.ClientTotalNumberOfReviews, opt => opt.Ignore()); // to be set manually;
                    }
                }
}
