using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IproposalService
    {

        Task<List<Proposal>> GetAllProposalsAsync();
        Task<Proposal> GetProposalByIdAsync(int id);
        Task<List<Proposal>> GetProposalsByProjectIdAsync(int projectId);
        Task<List<Proposal>> GetProposalsByFreelancerIdAsync(string freelancerId);


        Task<Proposal> UpdateProposalAsync(Proposal proposal);

        Task<Proposal> CreateProposalAsync(Proposal proposal);
        Task<bool> DeleteProposalAsync(int id);


    }
}
