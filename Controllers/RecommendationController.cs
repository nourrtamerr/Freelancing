using Freelancing.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    { // ✅ Inject services
        private readonly IAIService _aiService;
        private readonly IProjectRepository _projectRepository;

        // ✅ Constructor to receive them via Dependency Injection
        public RecommendationController(IAIService aiService, IProjectRepository projectRepository)
        {
            _aiService = aiService;
            _projectRepository = projectRepository;
        }

        // Sample endpoint to get recommended projects
        [HttpPost("suggested-projects")]
        public async Task<IActionResult> GetSuggestedProjects([FromBody] FreelancerForAI_DTO freelancerDto)
        {
            // Load all projects (with related skills)
            var allProjects = await _projectRepository.GetAllWithSkillsAsync();

            // Convert to AI-ready DTOs
            var aiProjects = allProjects.Select(MapToAI).ToList();

            var skillNames = freelancerDto.SkillNames
        .Where(name => !string.IsNullOrWhiteSpace(name))
        .ToList();

            var recommended = await _aiService.GetRecommendedProjectsAsync(skillNames, aiProjects);

            // Call Python AI service


            return Ok(recommended);
        }

        // Convert full Project entity → simplified DTO
        private ProjectForAI_DTO MapToAI(Project project)
        {
            return new ProjectForAI_DTO
            {
                Id = project.Id,
                Title = project.Title,
                RequiredSkills = project.ProjectSkills
                    .Select(ps => ps.Skill?.Name ?? "")
                    .Where(skill => !string.IsNullOrWhiteSpace(skill))
                    .ToList()
            };
        }
    }
}
