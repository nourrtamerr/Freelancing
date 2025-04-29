using AutoMapper;
using Freelancing.DTOs;
using Freelancing.IRepositoryService;

namespace Freelancing.RepositoryService
{
	public class ProjectService(ApplicationDbContext _context, IMapper mapper) : IProjectService
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
			if (project is not null)
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
			//var x = _context;
			//var y = _context.Set<Project>();
			//var z = _context.project;
			//var h = _context.project.ToList();
			var biddingProjects = await _context.Set<BiddingProject>()
									.Where(p => !p.IsDeleted)
									.ToListAsync();

			var fixedProjects = await _context.Set<FixedPriceProject>()
				.Where(p => !p.IsDeleted)
				.ToListAsync();

			var projects = biddingProjects.Cast<Project>()
				.Concat(fixedProjects.Cast<Project>())
				.ToList();
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