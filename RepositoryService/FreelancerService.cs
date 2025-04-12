using AutoMapper;
using Freelancing.DTOs.AuthDTOs;
using Freelancing.IRepositoryService;
using Freelancing.Migrations;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class FreelancerService(ApplicationDbContext context,IMapper _mapper) : IFreelancerService
    {
        public async Task<Freelancer> CreateFreelancerAsync(Freelancer freelancer)
        {
            Freelancer f = new Freelancer()
            {
                Certificates = freelancer.Certificates,
                UserName = freelancer.UserName,
                isAvailable = freelancer.isAvailable,
                Balance = freelancer.Balance,
                Education = freelancer.Education,
                Experiences = freelancer.Experiences,
                Languages = freelancer.Languages,
                Portofolio = freelancer.Portofolio,
                //IsDeleted = false
            };

            context.Add(f);
            context.SaveChanges();
            return f;
        }

        public async Task<bool> DeleteFreelancerAsync(string id)
        {
           var freelancer= await context.freelancers.FirstOrDefaultAsync(f => f.Id == id && !f.isDeleted);
            if (freelancer == null)
                return false;

            freelancer.isDeleted = true;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ViewFreelancersDTO>> GetAllAsync()
        {

            var freelancers= await context.freelancers.Include(f=>f.UserSkills).ThenInclude(S=>S.Skill).Where(f=>!f.isDeleted).ToListAsync();
            List<ViewFreelancersDTO> freelancerdtos = new();
            foreach(var freelancer in freelancers)
			{
                freelancerdtos.Add(_mapper.Map<ViewFreelancersDTO>(freelancer));
			}
            return freelancerdtos;
		}

        public async Task<ViewFreelancerPageDTO> GetByIDAsync(string id)
        {
            var freelancer=await context.freelancers.Include(f => f.UserSkills).ThenInclude(S => S.Skill)
                .Include(f=>f.Languages).Where(f => !f.isDeleted).FirstOrDefaultAsync(F=>F.Id==id);

			return _mapper.Map<ViewFreelancerPageDTO>(freelancer);

			
        }

        public async Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer)
        {
           var fl =  await context.freelancers.FirstOrDefaultAsync(f=>f.Id==freelancer.Id);

            if (fl == null)
            { 
                return null;

            }
              
            fl.firstname = freelancer.firstname;
            fl.lastname = freelancer.lastname;
            fl.City=freelancer.City;
            fl.Country=freelancer.Country;
            fl.ProfilePicture = freelancer.ProfilePicture;

            fl.isAvailable = freelancer.isAvailable;
            fl.Balance = freelancer.Balance;
            fl.Education = freelancer.Education;
            fl.Experiences = freelancer.Experiences;
            fl.Certificates = freelancer.Certificates;
            fl.UserSkills = freelancer.UserSkills;
            fl.Languages = freelancer.Languages;
            fl.Portofolio = freelancer.Portofolio;

            await context.SaveChangesAsync();
            return fl;


        }

        //Task<List<Freelancer>> IFreelancerService.GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Freelancer> IFreelancerService.GetByIDAsync(string id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
