using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class FreelancerLanguageService(ApplicationDbContext _context) : IFreelancerLanguageService
    {
        public async Task<bool> CreateLanguageAsync(FreelancerLanguage language)
        {
            await _context.freelancerLanguages.AddAsync(language);
            return await _context.SaveChangesAsync()>0;
        }

        public Task<bool> DeleteLanguageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Language> GetLanguageById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Language>> GetLanguagesByFreelancerUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLanguageAsync(FreelancerLanguage language)
        {
            throw new NotImplementedException();
        }
    }
}
