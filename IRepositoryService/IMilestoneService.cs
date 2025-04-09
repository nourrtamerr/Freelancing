using Freelancing.DTOs;

namespace Freelancing.IRepositoryService
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetAllAsync();

        Task<Milestone> GetByIdAsync(int id);

        Task<Milestone> CreateAsync(MilestoneDTO milestone);

        Task<Milestone> UpdateAsync(Milestone milestone);

        Task<bool> DeleteAsync(int id);


    }
}
