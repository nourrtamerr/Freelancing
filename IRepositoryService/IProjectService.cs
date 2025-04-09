namespace Freelancing.IRepositoryService
{
	public interface IProjectService
	{
		Task<List<Project>> GetAllProjectsAsync();
		Task<Project> GetProjectByIdAsync(int id);
		Task<Project> CreateProjectAsync(Project project);
		Task<Project> UpdateProjectAsync(Project project);
		Task<bool> DeleteProjectAsync(int id);
	}
}
