using Freelancing.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperienceService _experienceService;
        public ExperiencesController(IExperienceService experienceService)
        {
            _experienceService = experienceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExperiences()
        {
            var experienceslist = await _experienceService.GetAllExperiences();
            if (experienceslist == null || !experienceslist.Any())
            {
                return NotFound(new { msg = "No experiences found." });
            }
            var experienceDtolist = experienceslist.Select(e => new ExperienceDTO
            {
                JobTitle = e.JobTitle,
                isDeleted = e.isDeleted,
                Company = e.Company,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description,
                FreelancerId = e.FreelancerId
            });
            return Ok (experienceDtolist);
        }

        [HttpGet("freelancer/{id}")]
        public async Task<IActionResult> GetAllExperiencesByFreelancerId( string id)
        {
            var experienceslist = await _experienceService.GetExperienceByFreelancerId(id);
            if (experienceslist == null || !experienceslist.Any())
            {
                return NotFound(new { msg = "No experiences found." });
            }
            var experienceDtolist = experienceslist.Select(e => new ExperienceDTO
            {
                JobTitle = e.JobTitle,
                isDeleted = e.isDeleted,
                Company = e.Company,
                Location = e.Location,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Description = e.Description,
                FreelancerId = e.FreelancerId
            });
            return Ok (experienceDtolist);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            var experience = await _experienceService.GetExperienceById(id);
            if (experience == null)
            {
                return NotFound(new { msg = "no experince found has this id" });
            }
            var experienceDto = new ExperienceDTO
            {
                JobTitle = experience.JobTitle,
                isDeleted = experience.isDeleted,
                Company = experience.Company,
                Location = experience.Location,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                Description = experience.Description,
                FreelancerId = experience.FreelancerId
            };
            return Ok(experienceDto);
        }

        [HttpPost]
        [Authorize(Roles ="Freelancer")]
        public async Task<IActionResult> CreateExperience([FromBody] CreateExperienceDTO experienceDto)
        {
            //var freelancerid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var freelancername = User.FindFirstValue(ClaimTypes.Name);

			if (!ModelState.IsValid)
            {
                return BadRequest(experienceDto);
            }
            var exp = new Experience {
                JobTitle = experienceDto.JobTitle,
                isDeleted = false,
                Company = experienceDto.Company,
                Location = experienceDto.Location,
                StartDate = experienceDto.StartDate,
                EndDate = experienceDto.EndDate,
                Description = experienceDto.Description,
                FreelancerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            var created = await _experienceService.AddExperience(exp);
            if (created)
            {
                var createdExperience = await _experienceService.GetExperienceById(exp.Id);
                return CreatedAtAction(nameof(GetExperienceById), new { id = createdExperience.Id }, 
                    new
                    {
					createdExperience.Id,
                    createdExperience.Freelancer.UserName,
					createdExperience.JobTitle,
					createdExperience.Company,
					createdExperience.Location,
					createdExperience.StartDate,
					createdExperience.EndDate,
				    createdExperience.Description
					}
                    );
            }
            return BadRequest(new { msg = "failed to create experience" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] ExperienceDTO experienceDto)
        {
            var exp =await _experienceService.GetExperienceById(id);            
            if (exp == null || !ModelState.IsValid)
            {
                return BadRequest(new { msg = "can't find experience with this id"});
            }
            exp.StartDate = experienceDto.StartDate;
            exp.EndDate = experienceDto.EndDate;
            exp.Description = experienceDto.Description;
            exp.JobTitle = experienceDto.JobTitle;
            exp.Location = experienceDto.Location;
            exp.Company = experienceDto.Company;
            var updatedExperience = await _experienceService.UpdateExperience(exp);
            if (!updatedExperience)
            {
                return BadRequest(new {msg = "failed to update the experience" });
            }
            return Ok(updatedExperience);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var exp = await _experienceService.GetExperienceById(id);
            if(exp == null)
            {
                return BadRequest(new {msg="experience not found"});
            }
            var deleted = await _experienceService.DeleteExperience(id);
            if (!deleted)
            {
                return BadRequest(new { msg = "failed to delete experience"});
            }
            return Ok(new { msg = "experience marked as deleted successfully" });
        }        
                                        
    }
}
