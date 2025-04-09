
using Freelancing.Models;

namespace Freelancing.RepositoryService
{
	public class ExperienceService(ApplicationDbContext _context) : IExperienceService
	{
		public async Task<bool> AddExperience(Experience Experience)
		{
			await _context.Experiences.AddAsync(Experience);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteExperience(int id)
		{
			var exp = await _context.Experiences.FirstOrDefaultAsync(c => c.Id == id);
			if (exp == null)
			{
				return false;
			}
			exp.isDeleted = true;
			_context.Experiences.Update(exp);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> ExperienceExists(int id)
		{
			return await _context.Experiences.AnyAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Experience>> GetAllExperiences()
		{
			return await _context.Experiences.ToListAsync();
		}

		public async Task<IEnumerable<Experience>> GetExperienceByCompanyName(string name)
		{
			return await _context.Experiences.Where(e=>e.Company.Contains(name.Trim())).ToListAsync();
		}

		public async Task<IEnumerable<Experience>> GetExperienceByFreelancerId(string id)
		{
			return await _context.Experiences.Where(e => e.FreelancerId==id).ToListAsync();
		}

		public async Task<Experience> GetExperienceById(int id)
		{
			return await _context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<bool> UpdateExperience(Experience Experience)
		{
			var exp = await _context.Experiences.FirstOrDefaultAsync(c => c.Id == Experience.Id);
			if (exp == null)
			{
				return false;
			}
			_context.Experiences.Update(Experience);
			return await _context.SaveChangesAsync() > 0;

		}
	}
}
