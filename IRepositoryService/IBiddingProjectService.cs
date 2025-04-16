using Freelancing.DTOs.BiddingProjectDTOs;
using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IBiddingProjectService
    {
        Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync();
        Task<BiddingProjectGetByIdDTO> GetBiddingProjectByIdAsync(int id);
        Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectCreateUpdateDTO project, string ClinetId);
        Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProjectCreateUpdateDTO project, int projectDTOid);
        Task<bool> DeleteBiddingProjectAsync(int id);

        Task<List<BiddingProjectGetAllDTO>> Filter(BiddingProjectFilterDTO biddingProjectFilters);

    }
}
