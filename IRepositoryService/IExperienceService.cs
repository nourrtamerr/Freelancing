namespace Freelancing.IRepositoryService
{
	public interface IExperienceService
	{
		Task<bool> AddExperience(Experience Experience);
		Task<bool> UpdateExperience(Experience Experience);
		Task<bool> DeleteExperience(int id);
		Task<Experience> GetExperienceById(int id);
		Task<IEnumerable<Experience>> GetExperienceByFreelancerId(string id);
		Task<IEnumerable<Experience>> GetExperienceByCompanyName(string name);
		Task<IEnumerable<Experience>> GetAllExperiences();
		Task<bool> ExperienceExists(int id);
	}
}
