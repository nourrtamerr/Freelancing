using AutoMapper;
using Freelancing.DTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class PortofolioProjectService(ApplicationDbContext context, IMapper _mapper) : IPortofolioProject
    {

        public async Task<List<PortofolioProject>> GetAllAync()
        {
            return await context.PortofolioProjects.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<PortofolioProject> GetByIdAsync(int id)
        {
            return await context.PortofolioProjects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PortofolioProject>> GetByFreelancerId(string id)
        {
            var freelancer = context.freelancers.SingleOrDefault(f => f.Id == id);
            if(freelancer is not null)
            {
                var PortfolioProjects= await context.PortofolioProjects.Where(p => p.FreelancerId == id).ToListAsync();
                return PortfolioProjects;
            }
            throw new KeyNotFoundException("Invalid Freelancer Id");

        }




        public async Task<PortofolioProject> AddAsync(PortofolioProjectDTO portofolioProject)
        {
            PortofolioProject p = new PortofolioProject()
            {
                Title = portofolioProject.Title,
                Description = portofolioProject.Description,
                CreatedAt = portofolioProject.CreatedAt,
                Images = portofolioProject.Images,
                FreelancerId = portofolioProject.FreelancerId
            };
            await context.PortofolioProjects.AddAsync(p);
            await context.SaveChangesAsync();
            return p;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var portfolioPrject = context.PortofolioProjects.SingleOrDefault(p => p.Id == id);
            if(portfolioPrject is not null)
            {
                portfolioPrject.IsDeleted = true;
                context.PortofolioProjects.Update(portfolioPrject);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

   

        public async Task<PortofolioProject> UpdateAsync(PortofolioProjectDTO portofolioProject)
        {
            var p = context.PortofolioProjects.SingleOrDefault(p => p.Id == portofolioProject.Id);
            if (p is not null)
            {
                //p.Title = portofolioProject.Title;
                //p.Description = portofolioProject.Description;
                //p.CreatedAt = portofolioProject.CreatedAt;
                //p.Images = portofolioProject.Images;
                _mapper.Map(portofolioProject, p);

                await context.SaveChangesAsync();
                return p;
            }

            throw new KeyNotFoundException("Invalid Portfolio Project");
           
        }
    }
}
