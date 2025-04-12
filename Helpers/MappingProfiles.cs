using AutoMapper;
using Freelancing.DTOs;
using Freelancing.DTOs.AuthDTOs;

namespace Freelancing.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Milestone, MilestoneDTO>();
            CreateMap<MilestoneDTO, Milestone>();
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

            CreateMap<BiddingProjectDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectDTO>();

		}
            CreateMap<BiddingProjectGetAllDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectGetAllDTO>();
		}


        
    }
}
