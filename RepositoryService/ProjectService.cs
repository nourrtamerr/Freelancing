using Freelancing.IRepositoryService;

namespace Freelancing.RepositoryService
{
	public class ProjectService : IProjectService
	{
		public Task<Project> CreateProjectAsync(Project project)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteProjectAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Project>> GetAllProjectsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Project> GetProjectByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Project> UpdateProjectAsync(Project project)
		{
			throw new NotImplementedException();
		}
	}
}
