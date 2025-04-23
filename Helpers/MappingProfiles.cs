using AutoMapper;
using Freelancing.DTOs.AuthDTOs;
using Freelancing.DTOs.MilestoneDTOs;
using Freelancing.DTOs.ProposalDTOS;
using Freelancing.DTOs.BiddingProjectDTOs;
using Freelancing.DTOs;


namespace Freelancing.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Milestone, MilestoneGetAllDTO>();
            CreateMap<MilestoneGetAllDTO, Milestone>();
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
            CreateMap<AppUser, UsersRequestingVerificationViewDTO>()
                .ForMember(dest=>dest.Country, opt=> opt.MapFrom(src=>src.City.Country.Name))
                .ForMember(dest=>dest.City, opt=> opt.MapFrom(src=>src.City.Name));


            CreateMap<Freelancer, ViewFreelancersDTO>()
                .ForMember(dest => dest.UserSkills, opt =>
                opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).Union(src.NonRecommendedUserSkills.Select(us => us.Name)).ToList()))
				.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country.Name))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name)); 

            //CreateMap<Review, FreelancerReviewDTO>()
            //.ForMember(dest => dest.ReviewerName, opt => opt.MapFrom(src => src.Reviewer != null ? src.Reviewer.UserName : string.Empty));

			CreateMap<Freelancer, ViewFreelancerPageDTO>()
			   .ForMember(dest => dest.UserSkills, opt =>
			   opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).ToList()))
			   .ForMember(dest => dest.Languages, opt =>
			   opt.MapFrom(src => src.Languages.Select(us => us.Language.ToString()).ToList()))
			   .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country.Name))
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name));
            CreateMap<Freelancer, ViewFreelancerPageDTO>()
               .ForMember(dest => dest.UserSkills, opt =>
               opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).ToList()))
               .ForMember(dest => dest.Languages, opt =>
               opt.MapFrom(src => src.Languages.Select(us => us.Language.ToString()).ToList()));

            CreateMap<Client, ViewClientDTO>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => (src.City.Country.Name)))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => (src.City.Name)));

			CreateMap<AppUser, UsersViewDTO>()
            .ForMember(dest=>dest.role,opt=>opt.MapFrom(src=>(src.GetType()==typeof(Admin)?Accountrole.Admin :src.GetType()==typeof(Freelancer)?Accountrole.Freelancer:Accountrole.Client)))
            .ForMember(dest=>dest.Country,opt=>opt.MapFrom(src=>(src.City.Country.Name)))
            .ForMember(dest=>dest.City,opt=>opt.MapFrom(src=>(src.City.Name)))
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
					dest.Balance = (int?)client.Balance;

				}
			});

            CreateMap<ViewClientDTO, Client>();
            CreateMap<AppUser, UsersViewDTO>()
            .ForMember(dest => dest.role, opt => opt.MapFrom(src => (src.GetType() == typeof(Admin) ? Accountrole.Admin : src.GetType() == typeof(Freelancer) ? Accountrole.Freelancer : Accountrole.Client)))
            .AfterMap((src, dest) =>
            {
                if (src is Freelancer freelancer)
                {
                    dest.isAvailable = freelancer.isAvailable;
                    dest.Balance = (int?)freelancer.Balance;
                }
                if (src is Client client)
                {
                    dest.PaymentVerified = client.PaymentVerified;
                }
            });

            CreateMap<Freelancer, ViewFreelancerPageDTO>()
               .ForMember(dest => dest.UserSkills, opt =>
               opt.MapFrom(src => src.UserSkills.Select(us => us.Skill.Name).ToList()))
               .ForMember(dest => dest.Languages, opt =>
               opt.MapFrom(src => src.Languages.Select(us => us.Language.ToString()).ToList()))
			   .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.Country.Name))
				.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name));


			#endregion


			#region Proposal mappings

			CreateMap<CreateProposalSuggestedMilestoneDTO, SuggestedMilestone>();
			CreateMap<UpdateProposalSuggestedMilestoneDTO, SuggestedMilestone>();
            CreateMap<CreateProposalDTO, Proposal>()
            .ForMember(dest => dest.suggestedMilestones, opt => opt.MapFrom(src => src.suggestedMilestones))
            .ForMember(dest => dest.SuggestedDuration, opt => opt.MapFrom(src => src.SuggestedDuration));


            CreateMap<EditProposalDTO, Proposal>()
            .ForMember(dest => dest.suggestedMilestones, opt => opt.MapFrom(src => src.suggestedMilestones))
            .ForMember(dest => dest.SuggestedDuration, opt => opt.MapFrom(src => src.SuggestedDuration));

            CreateMap<SuggestedMilestone, MilestoneViewDTO>();

			CreateMap<Proposal, ProposalViewDTO>()
			.ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.UserName))
			.ForMember(dest => dest.FreelancerProfilePicture, opt => opt.MapFrom(src => src.Freelancer.ProfilePicture))
			.ForMember(dest => dest.Freelancerskills, opt => opt.MapFrom(src =>
				src.Freelancer.UserSkills
					.Where(us => !us.IsDelete)
					.Select(us => us.Skill.Name).ToList()))
			.ForMember(dest => dest.FreelancerLanguages, opt => opt.MapFrom(src =>
				src.Freelancer.Languages
					.Where(fl => !fl.IsDeleted)
					.Select(fl => fl.Language.ToString()).ToList()))
			.ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Freelancer.EmailConfirmed))
			.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Freelancer.City.Country.Name))
			.ForMember(dest => dest.SuggestedDuration, opt => opt.MapFrom(src => src.SuggestedDuration))
			.ForMember(dest => dest.suggestedMilestones, opt => opt.MapFrom(src => src.suggestedMilestones))
			.ForMember(dest => dest.rank, opt => opt.MapFrom(src => src.Freelancer.Rank));


            CreateMap<Proposal, ProposalViewDTO>()
            .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.UserName))
            
            .ForMember(dest => dest.FreelancerProfilePicture, opt => opt.MapFrom(src => src.Freelancer.ProfilePicture))
            
            .ForMember(dest => dest.Freelancerskills, opt => opt.MapFrom(src =>
                src.Freelancer.UserSkills
                    .Where(us => !us.IsDelete)
                    .Select(us => us.Skill.Name).ToList()))
            
            .ForMember(dest => dest.FreelancerLanguages, opt => opt.MapFrom(src =>
                src.Freelancer.Languages
                    .Where(fl => !fl.IsDeleted)
                    .Select(fl => fl.Language.ToString()).ToList()))
            
            .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Freelancer.EmailConfirmed))
            
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Freelancer.City.Country.Name))
            
            .ForMember(dest => dest.SuggestedDuration, opt => opt.MapFrom(src => src.SuggestedDuration))
            
            .ForMember(dest => dest.suggestedMilestones, opt => opt.MapFrom(src => src.suggestedMilestones))
            
            .ForMember(dest => dest.rank, opt => opt.MapFrom(src => src.Freelancer.Rank));

            #endregion
            CreateMap<BiddingProjectDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectDTO>();

            CreateMap<UserSkill, UserSkillDto>();
            CreateMap<UserSkillDto, UserSkill>();

            //CreateMap<PortofolioProject, PortofolioProjectDTO>();
            CreateMap<PortofolioProjectDTO, PortofolioProject>();

            CreateMap<PortofolioProjectImage, PortofolioProjectImageDTO>();
            CreateMap<PortofolioProjectImageDTO, PortofolioProjectImage>();



            CreateMap<PortofolioProject, PortofolioProjectDTO>()
    .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));


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
                .ForMember(dest => dest.ClientTotalNumberOfReviews, opt => opt.MapFrom(src => src.Client.Reviewed.Count()))

                .ForMember(dest => dest.ClientRating, opt => opt.MapFrom(src => src.Client.Reviewed.Average(r => r.Rating)))
                ;
                


            CreateMap<BiddingProjectGetByIdDTO, BiddingProject>();
            CreateMap<BiddingProject, BiddingProjectGetByIdDTO>()
                .ForMember(dest => dest.BidAveragePrice, opt => opt.MapFrom(src =>
                src.Proposals != null && src.Proposals.Any()
                ? (int)src.Proposals.Average(p => p.Price)
                : 0
                ))

                .ForMember(dest => dest.ProjectSkills, opt => opt.MapFrom(src => src.ProjectSkills.Select(ps => ps.Skill.Name)))

                .ForMember(dest => dest.PostedFrom, opt => opt.MapFrom(src =>
                (DateTime.Now - src.BiddingStartDate).Days))

                .ForMember(dest => dest.ClientTotalNumberOfReviews, opt => opt.MapFrom(src => src.Client.Reviewed.Count()))

                .ForMember(dest => dest.ClientRating, opt => opt.MapFrom(src => src.Client.Reviewed.Average(r => r.Rating)))

                .ForMember(dest => dest.FreelancersubscriptionPlan, opt => opt.MapFrom(src => src.Freelancer.subscriptionPlan.name))

                .ForMember(dest => dest.FreelancerTotalNumber, opt => opt.MapFrom(src => src.Freelancer.subscriptionPlan.TotalNumber))

                .ForMember(dest => dest.FreelancerRemainingNumberOfBids, opt => opt.MapFrom(src => src.Freelancer.RemainingNumberOfBids))
                ;



            //.ForMember(dest=>dest.projectSkills, opt=>opt.Ignore())
            //.ForMember(dest=>dest.Subcategory, opt=>opt.MapFrom(src=>src.Subcategory.Name.ToString()))

            CreateMap<BiddingProjectCreateUpdateDTO, BiddingProject>()
              //.ForMember(dest => dest.SubcategoryId, opt => opt.Ignore()) 
              //.ForMember(dest => dest.Subcategory, opt => opt.Ignore())
              //.ForMember(dest => dest.ProjectSkills, opt => opt.Ignore())
              .ForMember(dest => dest.experienceLevel, opt => opt.MapFrom(src => Enum.Parse<ExperienceLevel>(src.ExperienceLevel)))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
              .ForMember(dest => dest.ProjectSkills, opt => opt.MapFrom(src => src.ProjectSkillsIds.Select(ps => new ProjectSkill() { SkillId = ps })))
                .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.SubcategoryId));



            //CreateMap<BiddingProject, BiddingProjectCreateDTO>()
            //    .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.experienceLevel.ToString()))
            //    //.ForMember(dest => dest.SubcategoryName, opt => opt.Ignore())
            //    //.ForMember(dest => dest.ProjectSkills, opt => opt.Ignore())

            //;


            CreateMap<MilestoneGetAllDTO, Milestone>();

            CreateMap<Milestone, MilestoneGetByIdOrProjectIdDTO>(); //source, destination
            CreateMap<MilestoneGetByIdOrProjectIdDTO, Milestone>();
                //.ForMember(dest=>dest.Status, opt=>opt.MapFrom(src=> Enum.Parse<MilestoneStatus>(src.Status)));


            CreateMap<MilestoneCreateDTO, Milestone>();

        }
    }
}
