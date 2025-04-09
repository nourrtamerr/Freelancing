using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class CertificateService(ApplicationDbContext context) : ICertificatesService
    {
        public async Task<Certificate> CreateCertificateAsync(Certificate certificate)
        {
            Certificate newCertificate = new Certificate()
            {
                Name = certificate.Name,
                IsDeleted = certificate.IsDeleted,
                IssueDate = certificate.IssueDate,
                FreelancerId = certificate.FreelancerId
            };
            context.certificates.Add(newCertificate);
            await context.SaveChangesAsync();
            return newCertificate;
        }

        public async Task<bool> DeleteCertificateAsync(int id)
        {
            var certificate = await context.certificates.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (certificate == null)
            {
                return false;
            }
            certificate.IsDeleted = true;
            await context.SaveChangesAsync();
            return true;

           
        }

        public async Task<List<Certificate>> GetAllUserSkillAsync()
        {
            return await context.certificates.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Certificate> GetCertificateByIDAsync(int id)
        {
            return await context.certificates.FirstOrDefaultAsync(c => c.Id==id && !c.IsDeleted);
        }

        public async Task<Certificate> UpdateCertificateAsync(Certificate certificate)
        {
            var existingCertificate = await context.certificates.FirstOrDefaultAsync(c => c.Id == certificate.Id && !c.IsDeleted);

            if (existingCertificate == null)
            {
                return null;
              
            }

            existingCertificate.Name = certificate.Name;
            existingCertificate.IssueDate = certificate.IssueDate;
            existingCertificate.FreelancerId = certificate.FreelancerId;

            
            await context.SaveChangesAsync();
            return existingCertificate;


        }
    }
}
