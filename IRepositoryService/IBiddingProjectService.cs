using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IBiddingProjectService
    {
        Task<List<BiddingProject>> GetAllBiddingProjectsAsync();
        Task<BiddingProject> GetBiddingProjectByIdAsync(int id);
        Task<BiddingProject> CreateBiddingProjectAsync(BiddingProject project);
        Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProject project);
        Task<bool> DeleteBiddingProjectAsync(int id);



    }
}
