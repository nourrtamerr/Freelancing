using Freelancing.DTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class MilestoneService(ApplicationDbContext context) : IMilestoneService
    {

        public async Task<List<Milestone>> GetAllAsync()
        {
            return await context.Milestones.Where(m=>!m.IsDeleted).ToListAsync();
        }

        public async Task<Milestone> GetByIdAsync(int id)
        {
            return await context.Milestones.SingleOrDefaultAsync(m=>m.Id==id);
        }


        public async Task<Milestone> CreateAsync(MilestoneDTO milestone)
        {
            Milestone ms = new Milestone()
            {
                
                Title = milestone.Title,
                Description = milestone.Description,
                Status = milestone.Status,
                Amount = milestone.Amount,
                ProjectId = milestone.ProjectId,
                StartDate = milestone.StartDate,
                EndDate = milestone.EndDate,

            };

            await context.AddAsync(ms);
            await context.SaveChangesAsync();

            return ms;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var milestone = context.Milestones.SingleOrDefault(m => m.Id == id && !m.IsDeleted);
            if (milestone != null)
            {
                milestone.IsDeleted = true;
                context.Milestones.Update(milestone);
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }



        public async Task<Milestone> UpdateAsync(MilestoneDTO milestone)
        {
            var ms = context.Milestones.SingleOrDefault(m => m.Id == milestone.Id);
           
                ms.Title = milestone.Title;
                ms.Description = milestone.Description;
                ms.Amount = milestone.Amount;
                ms.ProjectId = milestone.ProjectId;
                ms.Status = milestone.Status;
                ms.StartDate = milestone.StartDate;
                ms.EndDate = milestone.EndDate;

                await context.SaveChangesAsync();
                return ms;
        }

        public async Task<List<Milestone>> GetByProjectId(int id)
        {
            var project = context.Projects.SingleOrDefault(p => p.Id == id);
            if(project is not null)
            {
                if(project.Milestones is not null)
                {
                    List<Milestone> milestones = context.Milestones.Where(m => m.ProjectId == id).ToList();
                    return milestones;
                }
                throw new InvalidOperationException("This project doesn't have any milestones.");
            }
            throw new KeyNotFoundException("Project not found.");
        }
    }
}
