using Freelancing.DTOs;
using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IBiddingProjectService
    {
        Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync();
        Task<BiddingProjectGetByIdDTO> GetBiddingProjectByIdAsync(int id);
        Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectCreateDTO project);
        Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProjectDTO project);
        Task<bool> DeleteBiddingProjectAsync(int id);



    }
}
