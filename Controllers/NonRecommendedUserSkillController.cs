using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonRecommendedUserSkillController(ApplicationDbContext context) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string SkillName)
        {
            NonRecommendedUserSkill NewSkill = new NonRecommendedUserSkill() { Name = SkillName };
            if (!context.nonRecommendedUserSkills.Any(s => s.Name == SkillName))
                context.nonRecommendedUserSkills.Add(NewSkill);
            else
            {
                NewSkill = context.nonRecommendedUserSkills.FirstOrDefault(s => s.Name == SkillName);
            }

            var freelancer = context.freelancers.FirstOrDefault(f => f.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (freelancer is not null)
            {

                freelancer.NonRecommendedUserSkills.Add(NewSkill);
                await context.SaveChangesAsync();
                return Ok(new { msg = "Skill added successfully" });
            }
            return BadRequest("Freelancer not found");
        }


        [HttpDelete("{SkillId}")]
        public async Task<IActionResult> Delete(int SkillId)
        {
            var freelancer = context.freelancers.FirstOrDefault(f => f.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (freelancer is not null)
            {
                var skill = freelancer.NonRecommendedUserSkills.FirstOrDefault(s => s.Id == SkillId);
                if (skill is not null)
                {
                    freelancer.NonRecommendedUserSkills.Remove(skill);
                    await context.SaveChangesAsync();
                    return Ok(new { msg = "Skill removed successfully" });
                }
                return BadRequest("Skill not found");

            }
            return BadRequest("Freelancer not found");

        }


    }
}
