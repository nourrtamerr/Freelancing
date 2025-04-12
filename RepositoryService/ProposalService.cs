using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class ProposalService : IproposalService
    {
     
        private readonly ApplicationDbContext _context;

        public ProposalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proposal>> GetAllProposalsAsync()
        {
           return await _context.Proposals
          .Include(p => p.Freelancer)
          .Include(p => p.Project)
          .ToListAsync();
        }

        public async Task<Proposal> GetProposalByIdAsync(int id)
        {
            // lazy loading
            return await _context.Proposals.FindAsync(id);

        }



        public async Task<List<Proposal>> GetProposalsByFreelancerIdAsync(string freelancerId)
        {
            return await _context.Proposals
            .Include(p => p.Project)
            .Where(p => p.FreelancerId == freelancerId)
            .ToListAsync();
        }

        public async Task<List<Proposal>> GetProposalsByProjectIdAsync(int projectId)
        {
            return await _context.Proposals
           .Include(p => p.Freelancer)
           .Where(p => p.ProjectId == projectId)
           .ToListAsync();
        }

        public async Task<Proposal> UpdateProposalAsync(Proposal proposal)
        {
            _context.Proposals.Update(proposal);
            await _context.SaveChangesAsync();
            return proposal;
        }



        public async Task<Proposal> CreateProposalAsync(Proposal proposal)
        {
            await _context.Proposals.AddAsync(proposal);
            await _context.SaveChangesAsync();
            return proposal;
        }

        public async Task<bool> DeleteProposalAsync(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null) return false;

            proposal.IsDeleted = true; 
            _context.Proposals.Update(proposal);
            await _context.SaveChangesAsync();
            return true;



        }
    }
}
