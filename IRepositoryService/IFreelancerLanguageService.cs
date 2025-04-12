using Microsoft.AspNetCore.Mvc;

namespace Freelancing.IRepositoryService
{
    public interface IFreelancerLanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
        Task<IEnumerable<Language>> GetLanguagesByFreelancerUserNameAsync(string username);
        Task<Language> GetLanguageById(int id);
        Task<bool> UpdateLanguageAsync(FreelancerLanguage language);
        Task<bool> CreateLanguageAsync(FreelancerLanguage language);
        Task<bool> DeleteLanguageAsync(int id);
    }
}
