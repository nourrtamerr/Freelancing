using Freelancing.DTOs;
using Freelancing.DTOs.MilestoneDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixedPriceProjectController : ControllerBase
    {


        private readonly IFixedProjectService _fixedProjectService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMilestoneService _milestoneService;
        private readonly UserManager<AppUser> _userManager;

		public FixedPriceProjectController(IFixedProjectService fixedProjectService, ApplicationDbContext dbContext,IMilestoneService milestoneService,UserManager<AppUser> userManager)
        {
            _fixedProjectService = fixedProjectService;
            _dbContext = dbContext;
            _milestoneService = milestoneService;
			_userManager = userManager;
		}



        [HttpGet("myfixedpriceprojects")]
        [Authorize]
		public async Task<ActionResult<IEnumerable<GetAllFixedProjectDto>>> myfixedpriceprojects()
        {
			var projects = await _fixedProjectService.GetAllFixedPriceProjectsAsync();
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound("User not found.");
			}
            if (user.GetType() == typeof(Client))
            {
				projects = projects.Where(p => p.ClientId == userId).ToList();
            }
			else if (user.GetType() == typeof(Freelancer))
			{
				projects = projects.Where(p => p.FreelancerId == userId).ToList();
			}
			else
			{
				return BadRequest("Invalid user type.");
			}
            return Ok(projects.Select(p => new GetAllFixedProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Currency = p.currency,
                ExpectedDuration = p.ExpectedDuration,
                SubcategoryID = p.SubcategoryId,
                ExperienceLevel = p.experienceLevel, // Convert enum to string if needed
                ProjectSkills = p.ProjectSkills != null
                    ? p.ProjectSkills.Where(ps => ps.Skill != null).Select(ps => ps.Skill.Name).ToList()
                    : new List<string>(),
                Milestones = p.Milestones?.Select(m => new MilestoneDto
                {
                    Title = m.Title,
                    startdate = m.StartDate,
                    enddate = m.EndDate,
                    Status = m.Status,
                }).ToList() ?? new List<MilestoneDto>(),
                ProposalsCount = p.Proposals.Count
            }).ToList());


		}


		[HttpGet("userfixedpriceprojects/{userId}")]
		public async Task<ActionResult<IEnumerable<GetAllFixedProjectDto>>> userfixedpriceprojects(string userId)
		{
			var projects = await _fixedProjectService.GetAllFixedPriceProjectsAsync();
			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound("User not found.");
			}
			if (user.GetType() == typeof(Client))
			{
				projects = projects.Where(p => p.ClientId == userId).ToList();
			}
			else if (user.GetType() == typeof(Freelancer))
			{
				projects = projects.Where(p => p.FreelancerId == userId).ToList();
			}
			else
			{
				return BadRequest("Invalid user type.");
			}
			return Ok(projects.Select(p => new GetAllFixedProjectDto
			{
				Id = p.Id,
				Title = p.Title,
				Description = p.Description,
				Currency = p.currency,
				ExpectedDuration = p.ExpectedDuration,
				SubcategoryID = p.SubcategoryId,
				ExperienceLevel = p.experienceLevel, // Convert enum to string if needed
				ProjectSkills = p.ProjectSkills != null
					? p.ProjectSkills.Where(ps => ps.Skill != null).Select(ps => ps.Skill.Name).ToList()
					: new List<string>(),
				Milestones = p.Milestones?.Select(m => new MilestoneDto
				{
					Title = m.Title,
					startdate = m.StartDate,
					enddate = m.EndDate,
					Status = m.Status,
				}).ToList() ?? new List<MilestoneDto>(),
				ProposalsCount = p.Proposals.Count
			}).ToList());


		}




		[HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllFixedProjectDto>>> GetAllFixedPriceProjects(
                 [FromQuery] int pageNumber = 1,
                 [FromQuery] int pageSize = 10,

//                 [FromQuery] ExperienceLevel? experienceLevel = null,
                [FromQuery] List<ExperienceLevel> experienceLevels = null,


                 [FromQuery] int? minProposals = null,
                 [FromQuery] int? maxProposals = null,

              [FromQuery] List<int> categoryIds = null,
                [FromQuery] List<int> subcategoryIds = null,

                 [FromQuery] Currency? currency = null,

                 [FromQuery] decimal? minPrice = null,
                 [FromQuery] decimal? maxPrice = null,

                 [FromQuery] int? minDuration = null,
                 [FromQuery] int? maxDuration = null,
                 [FromQuery] List<int> skillIds = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = await _fixedProjectService.GetAllFixedPriceProjectsAsync();

            if (projects == null || !projects.Any())
            {
                return NotFound("No fixed price projects found.");
            }

            //// Filter by experience level if specified
            //if (experienceLevel.HasValue)
            //{
            //    projects = projects.Where(p => p.experienceLevel == experienceLevel.Value).ToList();
            //}

            if (experienceLevels != null && experienceLevels.Any())
            {
                projects = projects
                    .Where(p => experienceLevels.Contains(p.experienceLevel))
                    .ToList();
            }


            // Filter by proposal count (min & max)
            if (minProposals.HasValue)
            {
                projects = projects.Where(p => p.Proposals.Count >= minProposals.Value).ToList();
            }

            if (maxProposals.HasValue)
            {
                projects = projects.Where(p => p.Proposals.Count <= maxProposals.Value).ToList();
            }

            // Filter by category and subcategory

            if (categoryIds != null && categoryIds.Any())
            {
                projects = projects
                    .Where(p => p.Subcategory != null &&
                                categoryIds.Contains(p.Subcategory.CategoryId))
                    .ToList();
            }

            if (subcategoryIds != null && subcategoryIds.Any())
            {
                projects = projects
                    .Where(p => subcategoryIds.Contains(p.SubcategoryId))
                    .ToList();
            }


            // Filter by currency
            if (currency.HasValue)
            {
                projects = projects.Where(p => p.currency == currency.Value).ToList();
            }


            // filter by min price
            if (minPrice.HasValue)
            {
                projects = projects
                    .Where(p => p is FixedPriceProject fpp && fpp.Price >= minPrice.Value)
                    .ToList();
            }
            // filter by max price

            if (maxPrice.HasValue)
            {
                projects = projects
                    .Where(p => p is FixedPriceProject fpp && fpp.Price <= maxPrice.Value)
                    .ToList();
            }


            // Filter by duration range
            if (minDuration.HasValue)
            {
                projects = projects.Where(p => p.ExpectedDuration >= minDuration.Value).ToList();
            }

            if (maxDuration.HasValue)
            {
                projects = projects.Where(p => p.ExpectedDuration <= maxDuration.Value).ToList();
            }


            // filter by projectskills

            if (skillIds != null && skillIds.Any())
            {
                projects = projects
                    .Where(p => p.ProjectSkills != null &&
                                p.ProjectSkills.Any(ps => skillIds.Contains(ps.SkillId)))
                    .ToList();
            }


            var projectDtos = projects.Select(p => new GetAllFixedProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Currency = p.currency,
                ExpectedDuration = p.ExpectedDuration,
                SubcategoryID = p.SubcategoryId,
                ExperienceLevel = p.experienceLevel, // Convert enum to string if needed
                ProjectSkills = p.ProjectSkills != null
                    ? p.ProjectSkills.Where(ps => ps.Skill != null).Select(ps => ps.Skill.Name).ToList()
                    : new List<string>(),
                Milestones = p.Milestones?.Select(m => new MilestoneDto
                {
                    Title = m.Title,
                    startdate = m.StartDate,
                    enddate = m.EndDate,
                    Status = m.Status,
                }).ToList() ?? new List<MilestoneDto>(),
                ProposalsCount = p.Proposals.Count
            }).ToList();

            // Apply pagination
            var totalItems = projectDtos;
            var pagedData = projectDtos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                TotalCount = totalItems.Count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Projects = pagedData
            };

            return Ok(response);
        }


        // pagination only

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GetAllFixedProjectDto>>> GetAllFixedPriceProjects(
        // [FromQuery] int pageNumber = 1,
        // [FromQuery] int pageSize = 10)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var projects = await _fixedProjectService.GetAllFixedPriceProjectsAsync();

        //    if (projects == null || !projects.Any())
        //    {
        //        return NotFound("No fixed price projects found.");
        //    }

        //    var projectDtos = projects.Select(p => new GetAllFixedProjectDto
        //    {
        //        Id = p.Id,
        //        Title = p.Title,
        //        Description = p.Description,
        //        Currency = p.currency,
        //        ExpectedDuration = p.ExpectedDuration,
        //        SubcategoryID = p.SubcategoryId,
        //        ExperienceLevel = p.experienceLevel,
        //        ProjectSkills = p.ProjectSkills != null
        //            ? p.ProjectSkills.Where(ps => ps.Skill != null).Select(ps => ps.Skill.Name).ToList()
        //            : new List<string>(),
        //        Milestones = p.Milestones?.Select(m => new MilestoneDto
        //        {
        //            Title = m.Title,
        //            startdate = m.StartDate,
        //            enddate = m.EndDate,
        //            Status = m.Status,
        //        }).ToList() ?? new List<MilestoneDto>(),
        //        ProposalsCount = p.Proposals.Count
        //    }).ToList();

        //    // Apply pagination
        //    var totalItems = projectDtos.Count;
        //    var pagedData = projectDtos
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();

        //    var response = new
        //    {
        //        TotalCount = totalItems,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        Projects = pagedData
        //    };

        //    return Ok(response);
        //}




        // without pagination

        //[HttpGet]
        //public async Task<ActionResult<List<GetAllFixedProjectDto>>> GetAllFixedPriceProjects()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var projects = await _fixedProjectService.GetAllFixedPriceProjectsAsync();

        //    if (projects == null || !projects.Any())
        //    {
        //        return NotFound("No fixed price projects found.");
        //    }
          

        //    var projectDto = projects.Select(p => new GetAllFixedProjectDto
        //    {
                
        //        Id = p.Id,
        //        Title = p.Title,
        //        Description = p.Description,
        //        Currency = p.currency,
        //        ExpectedDuration = p.ExpectedDuration,
        //        SubcategoryID = p.SubcategoryId,
               
        //        ExperienceLevel = p.experienceLevel,
              
        //        ProjectSkills = p.ProjectSkills != null
        //? p.ProjectSkills
        //    .Where(ps => ps.Skill != null)
        //    .Select(ps => ps.Skill.Name)
        //    .ToList()
        //: new List<string>(),



        //        Milestones = p.Milestones?.Select(m => new MilestoneDto
        //        {
        //            Title = m.Title,
        //            startdate = m.StartDate,
        //            enddate = m.EndDate,
        //            Status = m.Status,
        //        }).ToList() ?? new List<MilestoneDto>(),
        //        ProposalsCount =p.Proposals.Count// css  html
        //    }).ToList();


        //    return Ok(projectDto);
        //}


        [HttpGet("{id}")]
        public async Task<ActionResult<FixedPriceProject>> GetFixedPriceProjectById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _fixedProjectService.GetFixedPriceProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Fixed price project with ID {id} not found.");
            }

           
            var projectDto = new GetAllFixedProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Currency = project.currency,
                ExpectedDuration = project.ExpectedDuration,
              
                SubcategoryID = project.SubcategoryId,
                ExperienceLevel = project.experienceLevel,
                Milestones = project.Milestones?.Select(m => new MilestoneDto
                {

                    Title = m.Title,
                    startdate = m.StartDate,
                    enddate = m.EndDate,
                    Status = m.Status,

                }).ToList() ?? new List<MilestoneDto>(),

              

                ProjectSkills = project.ProjectSkills.Select(ps => ps.Skill.Name).ToList(),



                ProposalsCount = project.Proposals.Count
            };


            return Ok(projectDto);



        }




        [HttpPost]
        public async Task<ActionResult<GetAllFixedProjectDto>> CreateFixedPriceProject([FromBody] CreateFixedProjectDTO dto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = new FixedPriceProject
            {
                SubcategoryId = dto.SubcategoryID,
                Title = dto.Title,
                Description = dto.Description,
                currency = dto.Currency,
                ExpectedDuration = dto.ExpectedDuration,
                experienceLevel = dto.ExperienceLevel,
                Proposals = new List<Proposal>(), 
                ProjectSkills = new List<ProjectSkill>(), 
                Milestones = new List<Milestone>(),
                ClientId = "96668C77-FEBC-46A6-8B60-AF2AE1B7191B"

            };

            var createdProject = await _fixedProjectService.CreateFixedPriceProjectAsync(project);

            if (dto.ProjectSkills.Any())
            {
                foreach (var skillId in dto.ProjectSkills)
                {
                    var skill = await _dbContext.Skills.FindAsync(skillId);
                    if (skill != null)
                    {
                        project.ProjectSkills.Add(new ProjectSkill { SkillId = skill.Id ,
                            ProjectId = createdProject.Id


                        });
                    }
                }
            }

            if (dto.Milestones.Any())
            {
                foreach (var milestonedto in dto.Milestones)
                {

                    await _milestoneService.CreateAsync(new MilestoneCreateDTO()
                    {
                        Title = milestonedto.Title,
                        StartDate = milestonedto.startdate,
                        EndDate = milestonedto.enddate,
                        Status = (int)milestonedto.Status,
                        ProjectId  = createdProject.Id,
                        Amount = milestonedto.Amount,
                        Description = milestonedto.Description


                    });

                }
            }


            var projectDto = new GetAllFixedProjectDto
            {
                Id = createdProject.Id,
                Title = createdProject.Title,
                Description = createdProject.Description,
                Currency = createdProject.currency,
                ExpectedDuration = createdProject.ExpectedDuration,
                //SubcategoryName = createdProject.Subcategory?.Name,
                SubcategoryID = createdProject.SubcategoryId,
                ExperienceLevel = createdProject.experienceLevel,
                ProjectSkills = createdProject.ProjectSkills.Select(ps => ps.Skill.Name).ToList(),
                ProposalsCount = createdProject.Proposals.Count,
                Milestones = createdProject.Milestones?.Select(m => new MilestoneDto
                {

                    Title = m.Title,
                    startdate = m.StartDate,
                    enddate = m.EndDate,
                    Status = m.Status,

                }).ToList() ?? new List<MilestoneDto>()
            };



            // return CreatedAtAction(nameof(GetFixedPriceProjectById), new { id = createdProject.Id }, projectDto);
            return Ok( projectDto);

        }



      




        [HttpPut("{id}")]
        public async Task<ActionResult<GetAllFixedProjectDto>> UpdateFixedPriceProject(int id, [FromBody] CreateFixedProjectDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _fixedProjectService.GetFixedPriceProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Fixed price project with ID {id} not found.");
            }

            // Optional: Check Subcategory exists
            var subcategory = await _dbContext.Subcategories.FindAsync(dto.SubcategoryID);
            if (subcategory == null)
            {
                return BadRequest($"Subcategory with ID {dto.SubcategoryID} does not exist.");
            }

            // Update main fields
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.currency = dto.Currency;
            project.ExpectedDuration = dto.ExpectedDuration;
            project.experienceLevel = dto.ExperienceLevel;
            project.SubcategoryId = dto.SubcategoryID;

            // Update Milestones
            project.Milestones.Clear(); // Remove existing
            if (dto.Milestones.Any())
            {
                foreach (var milestoneDto in dto.Milestones)
                {
                    project.Milestones.Add(new Milestone
                    {
                        Title = milestoneDto.Title,
                        StartDate = milestoneDto.startdate,
                        EndDate = milestoneDto.enddate,
                        Status = milestoneDto.Status,
                        Description=milestoneDto.Description

                    });
                }
            }

            // Update ProjectSkills
            project.ProjectSkills.Clear(); // Remove existing
            if (dto.ProjectSkills.Any())
            {
                foreach (var skillId in dto.ProjectSkills)
                {
                    var skill = await _dbContext.Skills.FindAsync(skillId);

                    if (skill != null)
                    {
                        project.ProjectSkills.Add(new ProjectSkill
                        {
                            SkillId = skill.Id,
                            ProjectId = project.Id


                        });
                    }
                }
            }

            await _dbContext.SaveChangesAsync();

            // Map updated entity to DTO
            var updatedDto = new GetAllFixedProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Currency = project.currency,
                ExpectedDuration = project.ExpectedDuration,
                SubcategoryID = project.SubcategoryId,
              
                ExperienceLevel = project.experienceLevel,
                ProposalsCount = project.Proposals?.Count ?? 0,
                Milestones = project.Milestones?.Select(m => new MilestoneDto
                {
                    Title = m.Title,
                    startdate = m.StartDate,
                    enddate = m.EndDate,
                    Status = m.Status
                }).ToList() ?? new List<MilestoneDto>(),
                ProjectSkills = project.ProjectSkills != null
                    ? project.ProjectSkills
                        .Where(ps => ps.Skill != null)
                        .Select(ps => ps.Skill.Name)
                        .ToList()
                    : new List<string>()
            };

            return Ok(updatedDto);
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixedPriceProject(int id)
        {
            var project = await _fixedProjectService.GetFixedPriceProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound($"Fixed price project with ID {id} not found.");
            }

          await  _fixedProjectService.DeleteFixedPriceProjectAsync(id);
            await _dbContext.SaveChangesAsync();

            return Ok($"Fixed price project with ID {id} has been deleted successfully.");
        }







    }
}
