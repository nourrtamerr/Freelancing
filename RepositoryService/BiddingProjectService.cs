using AutoMapper;
using Freelancing.DTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class BiddingProjectService : IBiddingProjectService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BiddingProjectService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


            //return await _context.biddingProjects.Where(p => !p.IsDeleted)
            //    .Select(b => new BiddingProjectDTO
            //    {
            //        Title = b.Title,
            //        Description = b.Description,
            //        minimumPrice = b.minimumPrice,
            //        maximumprice = b.maximumprice,
            //        currency = b.currency,
            //        experienceLevel = b.experienceLevel,
            //        BidCurrentPrice = b.BidCurrentPrice,
            //        ProjectSkills = b.ProjectSkills.Select(ps => ps.Skill.Name).ToList(),
            //        ExpectedDuration = b.ExpectedDuration,
            //        PostedFrom = (DateTime.Now - b.BiddingStartDate).Days
            //    })
            //    .ToListAsync();

        public async Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync()
        {

            var projects = (await _context.biddingProjects
                                         .Include(p => p.Proposals)  
                                         .Include(p=>p.ProjectSkills)
                                         .ThenInclude(ps => ps.Skill)
                                         .Where(p => !p.IsDeleted )
                                         .ToListAsync())
                                         .Where(p=>p.Status==projectStatus.Pending)
                                         .ToList();


           
            
            var projectsDTO = _mapper.Map<List<BiddingProjectGetAllDTO>>(projects);
            foreach (var dto in projectsDTO)
            {
                var original = projects.FirstOrDefault(p => p.Id == dto.Id);
                dto.PostedFrom = (DateTime.Now - original.BiddingStartDate).Days;
                dto.ProjectSkills = original.ProjectSkills.Select(ps => ps.Skill.Name).ToList();
                var proposalCount = original.Proposals?.Count() ?? 0;
                if (proposalCount > 0)
                {
                    dto.BidAveragePrice = (int)(original.Proposals.Sum(s => s.Price) / proposalCount);
                }
                else
                {
                    dto.BidAveragePrice = 0;
                }

                dto.ClientRating= original.Client.Reviews.Select(r=>r.)

            }

            return projectsDTO;
            
        }

        public async Task<BiddingProject> GetBiddingProjectByIdAsync(int id)
        {
            return await _context.biddingProjects.FirstOrDefaultAsync(p => p.Id == id);
        }



        public async Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectDTO project)
        {
            BiddingProject p = _mapper.Map<BiddingProject>(project);

            await _context.biddingProjects.AddAsync(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public async Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProjectDTO project)
        {
            var p = _context.biddingProjects.SingleOrDefault(p => p.Id == project.Id);
            if(p is not null)
            {
                _mapper.Map(project, p);
                //_context.biddingProjects.Update(p);
                await _context.SaveChangesAsync();

            }
            return p;
        }

        public async Task<bool> DeleteBiddingProjectAsync(int id)
        {
            var project = await _context.biddingProjects.FindAsync(id);
            if (project == null) return false;

            //_context.biddingProjects.Remove(project);
            project.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
