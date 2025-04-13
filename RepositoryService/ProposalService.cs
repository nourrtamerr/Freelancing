﻿using AutoMapper;
using Freelancing.DTOs.ProposalDTOS;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class ProposalService(IMapper _mapper, ApplicationDbContext _context) : IproposalService
    {
     

        public async Task<List<ProposalViewDTO>> GetAllProposalsAsync()
        {
			var proposals = await _context.Proposals
							 .Include(p => p.suggestedMilestones)
							 .Include(p => p.Freelancer)
							 .ThenInclude(f => f.UserSkills)
							 .ThenInclude(us => us.Skill)
							 .Include(p => p.Freelancer)
							 .ThenInclude(f => f.Languages)
							 .Include(f => f.Freelancer.Reviewed)
							 .ToListAsync();

			
			var proposalDto = _mapper.Map<List<ProposalViewDTO>>(proposals);

			return proposalDto;
		}

        public async Task<ProposalViewDTO> GetProposalByIdAsync(int id)
        {
			// lazy loading
			    var proposal = await _context.Proposals
	                        .Include(p => p.suggestedMilestones)
                            .Include(p => p.Freelancer)
                            .ThenInclude(f => f.UserSkills)
                            .ThenInclude(us => us.Skill)
                            .Include(p => p.Freelancer)
                            .ThenInclude(f => f.Languages)
                            .Include(f=>f.Freelancer.Reviewed)
                            .FirstOrDefaultAsync(p => p.Id == id);

			if (proposal == null)
			{
				return null;
			}
			var proposalDto = _mapper.Map<ProposalViewDTO>(proposal);

			return proposalDto;


		}



        public async Task<List<ProposalViewDTO>> GetProposalsByFreelancerIdAsync(string freelancerId)
        {



			var proposal = await _context.Proposals
						   .Include(p => p.suggestedMilestones)
						   .Include(p => p.Freelancer)
						   .ThenInclude(f => f.UserSkills)
						   .ThenInclude(us => us.Skill)
						   .Include(p => p.Freelancer)
						   .ThenInclude(f => f.Languages)
						   .Include(f => f.Freelancer.Reviewed)
						   .Where(p => p.FreelancerId == freelancerId)
			.ToListAsync();

			
			var proposalDto = _mapper.Map<List<ProposalViewDTO>>(proposal);

			return proposalDto;
		}

        public async Task<List<ProposalViewDTO>> GetProposalsByProjectIdAsync(int projectId)
        {
 


			var proposal = await _context.Proposals
						   .Include(p => p.suggestedMilestones)
						   .Include(p => p.Freelancer)
						   .ThenInclude(f => f.UserSkills)
						   .ThenInclude(us => us.Skill)
						   .Include(p => p.Freelancer)
						   .ThenInclude(f => f.Languages)
						   .Include(f => f.Freelancer.Reviewed)
							.Where(p => p.ProjectId == projectId)
							.ToListAsync();

			
			var proposalDto = _mapper.Map<List<ProposalViewDTO>>(proposal);

			return proposalDto;
		}

        public async Task<ProposalViewDTO> UpdateProposalAsync(int id,EditProposalDTO proposaldto)
        {
            var proposal = await GetProposalByIdAsync(id);
            foreach(var suggestedmileston in proposal.suggestedMilestones)
            {
           
                _context.Remove(await _context.suggestedMilestones.FirstOrDefaultAsync(f=>f.Id== suggestedmileston.id));
            }
            await _context.SaveChangesAsync();
            var theproposal= await _context.Proposals.FirstOrDefaultAsync(f => f.Id == id);
			_mapper.Map(proposaldto, theproposal);
            _context.Update(theproposal);
            await _context.SaveChangesAsync();
            return proposal;
        }



        public async Task<ProposalViewDTO> CreateProposalAsync(CreateProposalDTO proposaldto,string freelancerId)
        {
            var proposal=_mapper.Map<Proposal>(proposaldto);
            proposal.FreelancerId = freelancerId;
			await _context.Proposals.AddAsync(proposal);
            await _context.SaveChangesAsync();
            return await GetProposalByIdAsync(proposal.Id);
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
