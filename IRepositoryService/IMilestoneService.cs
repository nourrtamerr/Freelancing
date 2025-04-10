using Freelancing.DTOs;

namespace Freelancing.IRepositoryService
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetAllAsync();

        Task<Milestone> GetByIdAsync(int id);

        Task<List<Milestone>> GetByProjectId(int id);

        Task<Milestone> CreateAsync(MilestoneDTO milestone);

        Task<Milestone> UpdateAsync(MilestoneDTO milestone);

        Task<bool> DeleteAsync(int id);


    }
}
