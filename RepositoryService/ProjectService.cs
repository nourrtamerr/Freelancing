using AutoMapper;
using Freelancing.DTOs;
using Freelancing.IRepositoryService;

namespace Freelancing.RepositoryService
{
	public class ProjectService(ApplicationDbContext _context,IMapper mapper) : IProjectService
	{
		public async Task<Project> CreateProjectAsync(Project project)
		{
			//await _context.project.AddAsync(project);
			await _context.SaveChangesAsync();
			return project;
		}

		public async Task<bool> DeleteProjectAsync(int id)
		{
			var project = await GetProjectByIdAsync(id);
			if(project is not null)
			{
				project.IsDeleted = true;
				_context.Update(project);
			}
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<List<Project>> GetAllProjectsAsync()
		{
			return await _context.Set<Project>().ToListAsync();
		}

		public async Task<Project> GetProjectByIdAsync(int id)
		{
			return await _context.Set<Project>().FindAsync(id);
		}

		public async Task<Project> UpdateProjectAsync(Project project)
		{
			var proj = await GetProjectByIdAsync(project.Id);
			if (proj is not null)
			{
				_context.Update(project);
			}
            return proj;
        }


        public async Task<List<ProjectDTO>> GetAllProjectsDtoAsync()
        {
            var projects = await _context.Set<Project>()
                                 .Where(p => !p.IsDeleted)
                                 .ToListAsync();
            return mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetProjectDtoByIdAsync(int id)
        {
            var project = await _context.Set<Project>()
                                 .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            return mapper.Map<ProjectDTO>(project);
        }
    }
}
