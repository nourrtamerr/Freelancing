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

			CreateMap<Client, ViewClientDTO>();
			CreateMap<ViewClientDTO, Client>();
			CreateMap<AppUser, UsersViewDTO>()
            .ForMember(dest=>dest.role,opt=>opt.MapFrom(src=>(src.GetType()==typeof(Admin)?Accountrole.Admin :src.GetType()==typeof(Freelancer)?Accountrole.Freelancer:Accountrole.Client)))
			.AfterMap((src, dest) =>
			{
				if (src is Freelancer freelancer)
				{
					dest.isAvailable = freelancer.isAvailable;
					dest.Balance = (int?)freelancer.Balance;
				}
                if(src is Client client)
				{
					dest.PaymentVerified = client.PaymentVerified;
				}
			});
			//CreateMap<ViewClientDTO, Client>();


			#endregion

			CreateMap<BiddingProjectDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectDTO>();

		}        
    }
}
