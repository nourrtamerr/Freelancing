using AutoMapper;
using Freelancing.DTOs;
using Freelancing.DTOs.BiddingProjectDTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Freelancing.RepositoryService
{
    public class BiddingProjectService : IBiddingProjectService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISubcategoryService _subcategoryService;

        public BiddingProjectService(ApplicationDbContext context, IMapper mapper, ISubcategoryService subcategoryService)
        {
            _context = context;
            _mapper = mapper;
            _subcategoryService = subcategoryService;
        }



        public async Task<List<BiddingProjectGetAllDTO>> GetAllBiddingProjectsAsync()
        {

            var projects = (await _context.biddingProjects

                                         .Include(p => p.Proposals)
                                         .Include(p => p.ProjectSkills)
                                         .ThenInclude(ps => ps.Skill)
                                         .Include(b => b.Client).ThenInclude(c => c.Reviewed)
                                         .Where(p => !p.IsDeleted)
                                         .ToListAsync())
                                         .Where(p => p.Status == projectStatus.Pending)
                                         .ToList();

            var projectsDTO = _mapper.Map<List<BiddingProjectGetAllDTO>>(projects);

            //foreach (var dto in projectsDTO)
            //{
            //    var original = projects.FirstOrDefault(p => p.Id == dto.Id);

            //    var clientReviews = await _context.Reviews.Where(r => r.RevieweeId == original.ClientId).ToListAsync();

            //    dto.ClientRating = clientReviews.Any()
            //        ? (clientReviews.Average(r => r.Rating))
            //        : 0;

            //    dto.ClientTotalNumberOfReviews = clientReviews.Count;

            //}

            return projectsDTO;

        }


        //----------------------------------------------------------------------------------------------------

        public async Task<BiddingProjectGetByIdDTO> GetBiddingProjectByIdAsync(int id)
        {
            var project = await _context.biddingProjects
                                        .Include(b => b.Proposals)
                                        .Include(b => b.ProjectSkills).ThenInclude(ps => ps.Skill)
                                        .Include(b => b.Client).ThenInclude(c => c.Reviewed)
                                        .Include(b=>b.Freelancer).ThenInclude(f=>f.subscriptionPlan)
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


        //var subcategory = await _context.Subcategories.FirstOrDefaultAsync(s => s.Name.ToLower() == project.SubcategoryName.ToLower());

        //if (subcategory == null)
        //    throw new Exception($"Subcategory not found");


        //var skillEntities = await _context.Skills.Where(s => project.ProjectSkills.Select(ps => ps.ToLower()).Contains(s.Name.ToLower())).ToListAsync();

        //if (skillEntities == null)
        //    throw new Exception($"projectskills not found");


        //createdProject.Subcategory = subcategory;
        //createdProject.CreatedAt = DateTime.Now;
        //createdProject.ProjectSkills = skillEntities.Select(skill => new ProjectSkill
        //{
        //    SkillId = skill.Id,
        //    Skill = skill
        //}).ToList();

        public async Task<BiddingProject> CreateBiddingProjectAsync(BiddingProjectCreateUpdateDTO project, string ClinetId)
        {



            var createdProject = _mapper.Map<BiddingProject>(project);
            createdProject.ClientId = ClinetId;



            await _context.biddingProjects.AddAsync(createdProject);
            await _context.SaveChangesAsync();

            _context.Entry(createdProject).Reference(p => p.Subcategory).Load();
            await _context.Entry(createdProject).Collection(p => p.ProjectSkills).LoadAsync();
            foreach (var projectSkill in createdProject.ProjectSkills)
            {
                _context.Entry(projectSkill).Reference(ps => ps.Skill).Load();
            }
            return createdProject;
        }


        //----------------------------------------------------------------------------------------------------



        public async Task<BiddingProject> UpdateBiddingProjectAsync(BiddingProjectCreateUpdateDTO project, int id)
        {
            var p = _context.biddingProjects.Include(b=>b.ProjectSkills).ThenInclude(ps=>ps.Skill).Include(p=>p.Subcategory).SingleOrDefault(p => p.Id == id);
            if (p is not null)
            {
                foreach(var skill in p.ProjectSkills)
                {
                    _context.ProjectSkills.Remove(skill);
                }
                _context.SaveChanges();
                _mapper.Map(project, p);
                //_context.biddingProjects.Update(p);
                await _context.SaveChangesAsync();

            }
            _context.Entry(p).Reference(p => p.Subcategory).Load();
            await _context.Entry(p).Collection(ps => ps.ProjectSkills).LoadAsync();
            foreach (var projectSkill in p.ProjectSkills)
            {
                _context.Entry(projectSkill).Reference(ps => ps.Skill).Load();
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


        //----------------------------------------------------------------------------------------------------


        public async Task<List<BiddingProjectGetAllDTO>> Filter(BiddingProjectFilterDTO filters)
        {
            var query = _context.biddingProjects.Include(b=>b.Subcategory)
                                                .ThenInclude(s=>s.Category)
                                                .Include(b=>b.ProjectSkills)
                                                .ThenInclude(ps=>ps.Skill)
                                                .Include(b=>b.Proposals)
                                                .Include(b => b.Client).ThenInclude(c => c.Reviewed)
                                                .Where(b => !b.IsDeleted).AsQueryable();

            if (filters.minPrice > 0)
            {
                query = query.Where(q => q.minimumPrice == filters.minPrice);
            }

            if (filters.maxPrice > 0)
            {
                query = query.Where(b => b.maximumprice == filters.maxPrice);
            }

            if(filters.Currency is { Count: > 0 })
            {
                query = query.Where(b => filters.Currency.Contains((int)b.currency));
            }

            if(filters.Category is { Count: > 0 })
            {
                query = query.Where(b => filters.Category.Contains(b.Subcategory.Category.Id));
            }

            if(filters.SubCategory is { Count: > 0 })
            {
                query = query.Where(b => filters.SubCategory.Contains(b.SubcategoryId));
            }

            if(filters.Skills is { Count: > 0 })
            {
                query = query.Where(b=>b.ProjectSkills.Any(ps=>filters.Skills.Contains(ps.SkillId)));
            }

            if(filters.ExperienceLevel is { Count: > 0 })
            {
                query = query.Where(b => filters.ExperienceLevel.Contains((int)b.experienceLevel));
            }

            if (filters.MinNumOfProposals > 0)
            {
                query = query.Where(b => b.Proposals.Count() >= filters.MinNumOfProposals);
            }

            if(filters.MaxNumOfProposals > 0)
            {
                query = query.Where(b => b.Proposals.Count() <= filters.MaxNumOfProposals);

            }



            if (filters.MinExpectedDuration > 0)
            {
                query = query.Where(b => b.ExpectedDuration >= filters.MinExpectedDuration);
            }

            if (filters.MaxExpectedDuration > 0)
            {
                query = query.Where(b => b.ExpectedDuration <= filters.MaxExpectedDuration);
            }

            var result = await query.ToListAsync();

            var filterdto = _mapper.Map<List<BiddingProjectGetAllDTO>>(result);




            return filterdto;

        }
        //foreach (var dto in filterdto)
        //{
        //    var original = query.FirstOrDefault(p => p.Id == dto.Id);

        //    var clientReviews = await _context.Reviews.Where(r => r.RevieweeId == original.ClientId).ToListAsync();

        //    dto.ClientRating = clientReviews.Any()
        //        ? (clientReviews.Average(r => r.Rating))
        //        : 0;

        //    dto.ClientTotalNumberOfReviews = clientReviews.Count;

        //}
    }
}
