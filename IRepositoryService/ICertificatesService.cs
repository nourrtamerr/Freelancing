namespace Freelancing.IRepositoryService
{
    public interface ICertificatesService
    {
        Task<List<Certificate>> GetAllUserSkillAsync();
        Task<Certificate> GetCertificateByIDAsync(int id);
        Task<Certificate> UpdateCertificateAsync(Certificate certificate);
        Task<Certificate> CreateCertificateAsync(Certificate certificate);
        Task<bool> DeleteCertificateAsync(int id);
    }
}
