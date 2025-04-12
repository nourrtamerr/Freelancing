using Freelancing.IRepositoryService;
using Freelancing.Migrations;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class FreelancerService(ApplicationDbContext context) : IFreelancerService
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

        public async Task<List<Freelancer>> GetAllAsync()
        {
            return await context.freelancers.Where(f=>!f.isDeleted).ToListAsync();
        }

        public async Task<Freelancer> GetByIDAsync(string id)
        {
            var freelancer = await context.freelancers.FirstOrDefaultAsync(f => f.Id == id && !f.isDeleted);
            return freelancer;
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
    }
}
