using Freelancing.DTOs;
using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IBiddingProjectService
    {
        Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync();
        Task<BiddingProject> GetBiddingProjectByIdAsync(int id);
        Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectDTO project);
        Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProjectDTO project);
        Task<bool> DeleteBiddingProjectAsync(int id);



    }
}
