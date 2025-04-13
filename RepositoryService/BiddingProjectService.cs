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



        public async Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync()
        {


    
                //    dto.PostedFrom = (DateTime.Now - original.BiddingStartDate).Days;
                //    dto.ProjectSkills = original.ProjectSkills.Select(ps => ps.Skill.Name).ToList();
                //    var proposalCount = original.Proposals?.Count() ?? 0;
                //    if (proposalCount > 0)
                //    {
                //        dto.BidAveragePrice = (int)original.Proposals.Average(s => s.Price);
                //    }
                //    else
                //    {
                //        dto.BidAveragePrice = 0;
                //    }

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

                var clientReviews = await _context.Reviews.Where(r => r.RevieweeId == original.ClientId).ToListAsync();

                dto.ClientRating = clientReviews.Any()
                    ? (clientReviews.Average(r => r.Rating))
                    : 0;

                dto.ClientTotalNumberOfReviews = clientReviews.Count;

            }

            return projectsDTO;
            
        }


        //----------------------------------------------------------------------------------------------------

        public async Task<BiddingProjectGetByIdDTO> GetBiddingProjectByIdAsync(int id)
        {
            var project = await _context.biddingProjects
                                        .Include(b=>b.Proposals)
                                        .Include(b=>b.ProjectSkills).ThenInclude(ps=>ps.Skill)
                                        .Include(b=>b.Client).ThenInclude(c=>c.Reviewed)                             
                                        .FirstOrDefaultAsync(b => b.Id == id);

            var projectDto = _mapper.Map<BiddingProjectGetByIdDTO>(project);

            return projectDto;

        }


        //----------------------------------------------------------------------------------------------------


            //var projectskills= await _context.ProjectSkills.Include(ps=>ps.Skill.Name).Where(p=>p.Skill.Name.)

            // List<string> ps = new List<string>();
            //foreach(var skill in project.projectSkills)
            //{
            //    ps.Add(skill);
            //}
        public async Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectCreateDTO project)
        {
            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(s => s.Name == project.SubCategoryName);

            var skillEntities = await _context.Skills.Where(s => project.projectSkills.Contains(s.Name)).ToListAsync();

            if (subcategory == null)
                throw new Exception("Subcategory not found");

            
            var CreatedProject= _mapper.Map<BiddingProject>(project);

            CreatedProject.Subcategory = subcategory;
            CreatedProject.ProjectSkills = skillEntities.Select(skill => new ProjectSkill
            {
                SkillId = skill.Id,
                Skill = skill
            }).ToList();

            await _context.biddingProjects.AddAsync(CreatedProject);
            await _context.SaveChangesAsync();

            return CreatedProject;
        }


        //----------------------------------------------------------------------------------------------------



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



        //----------------------------------------------------------------------------------------------------


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
