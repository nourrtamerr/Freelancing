using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IFixedProjectService
    {
        Task<List<FixedPriceProject>> GetAllFixedPriceProjectsAsync();
        Task<FixedPriceProject> GetFixedPriceProjectByIdAsync(int id);
        Task<FixedPriceProject> CreateFixedPriceProjectAsync(FixedPriceProject project);
        Task<FixedPriceProject> UpdateFixedPriceProjectAsync(FixedPriceProject project);
        Task<bool> DeleteFixedPriceProjectAsync(int id);


    }
}
