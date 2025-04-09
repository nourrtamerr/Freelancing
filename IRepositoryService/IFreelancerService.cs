using Freelancing.Models;

namespace Freelancing.IRepositoryService
{
    public interface IFreelancerService
    {
        Task<List<Freelancer>> GetAllAsync();
        Task<Freelancer> GetByIDAsync(string id);
        Task<Freelancer> UpdateFreelancerAsync(Freelancer freelancer);
        Task<Freelancer> CreateFreelancerAsync(Freelancer freelancer);
        Task<bool> DeleteFreelancerAsync(string id);


    }
}
