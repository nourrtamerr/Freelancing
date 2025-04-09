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
            return await context.Milestones.ToListAsync();
        }

        public async Task<Milestone> GetByIdAsync(int id)
        {
            return await context.Milestones.FindAsync(id);
        }


        public async Task<Milestone> CreateAsync(MilestoneDTO milestone)
        {
            Milestone ms = new Milestone()
            {
                Id = milestone.Id,
                Title = milestone.Title,
                Description = milestone.Description,
                Status = milestone.Status,
                Amount = milestone.Amount,
                ProjectId = milestone.ProjectId,
                StartDate = milestone.StartDate,
                EndDate = milestone.EndDate,

            };

            context.Add(ms);
            context.SaveChanges();

            return ms;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }



        public Task<Milestone> UpdateAsync(Milestone milestone)
        {
            throw new NotImplementedException();
        }
    }
}
