using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;


namespace Freelancing.RepositoryService
{
    public class FixedProjectService : IFixedProjectService
    {

        private readonly ApplicationDbContext _context;

        public FixedProjectService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<FixedPriceProject> CreateFixedPriceProjectAsync(FixedPriceProject project)
        {
            await _context.fixedPriceProjects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }





        public async Task<List<FixedPriceProject>> GetAllFixedPriceProjectsAsync()
        {
            return await _context.Set<FixedPriceProject>().ToListAsync();
        }




        public async Task<FixedPriceProject> GetFixedPriceProjectByIdAsync(int id)
        {
            return await _context.Set<FixedPriceProject>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<FixedPriceProject> UpdateFixedPriceProjectAsync(FixedPriceProject project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            _context.Set<FixedPriceProject>().Update(project);
            await _context.SaveChangesAsync();
            return project;

        }




        public async Task<bool> DeleteFixedPriceProjectAsync(int id)
        {
            var project = await _context.Set<FixedPriceProject>().FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _context.Set<FixedPriceProject>().Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
