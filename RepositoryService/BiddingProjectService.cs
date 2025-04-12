using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class BiddingProjectService : IBiddingProjectService
    {

        private readonly ApplicationDbContext _context;

        public BiddingProjectService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<BiddingProject>> GetAllBiddingProjectsAsync()
        {
            return await _context.biddingProjects.ToListAsync();
        }

        public async Task<BiddingProject> GetBiddingProjectByIdAsync(int id)
        {
            return await _context.biddingProjects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BiddingProject> CreateBiddingProjectAsync(BiddingProject project)
        {
            await _context.biddingProjects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProject project)
        {
            _context.biddingProjects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteBiddingProjectAsync(int id)
        {
            var project = await _context.biddingProjects.FindAsync(id);
            if (project == null) return false;

            _context.biddingProjects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
