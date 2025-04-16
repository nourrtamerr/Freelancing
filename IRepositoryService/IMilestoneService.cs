using Freelancing.DTOs.MilestoneDTOs;

namespace Freelancing.IRepositoryService
{
    public interface IMilestoneService
    {
        Task<List<MilestoneGetAllDTO>> GetAllAsync();

        Task<MilestoneGetByIdOrProjectIdDTO> GetByIdAsync(int id);

        Task<List<MilestoneGetByIdOrProjectIdDTO>> GetByProjectId(int ProjectId);

        Task<MilestoneCreateDTO> CreateAsync(MilestoneCreateDTO milestone);

        /*Task<Milestone> UpdateStatusAsync(MilestoneGetAllDTO milestone);*/ //for status only => httppatch


        Task<MilestoneGetAllDTO> UpdateStatusAsync(int MilestoneId, int StatusId);


        Task<MilestoneGetAllDTO> UpdateAsync(MilestoneGetByIdOrProjectIdDTO milestone);

        Task<bool> DeleteAsync(int id);




    }
}
