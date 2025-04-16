using AutoMapper;
using Freelancing.DTOs;
using Freelancing.Models;
using Freelancing.RepositoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectSkillsController : ControllerBase
    {
        private readonly IProjectSkillRepository _projectSkillRepo;
        private readonly IMapper _mapper;

        public ProjectSkillsController(IProjectSkillRepository projectSkillRepo, IMapper mapper)
        {
            _projectSkillRepo = projectSkillRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projectSkills = await _projectSkillRepo.GetAllAsync();
            var projectSkillDtos = _mapper.Map<List<ProjectSkillDto>>(projectSkills);
            return Ok(projectSkillDtos);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles ="client")]
        public async Task<IActionResult> GetById(int id)
        {
            var projectSkill = await _projectSkillRepo.GetByIdAsync(id);
            if (projectSkill == null)
                return NotFound();

            var projectSkillDto = _mapper.Map<ProjectSkillDto>(projectSkill);
            return Ok(projectSkillDto);
        }

        [HttpPost]
        //[Authorize(Roles ="client")]

        public async Task<IActionResult> AddSkillsToProject([FromBody] ProjectSkillCreateDto createDto)
        {
            if (createDto == null)
                return BadRequest();

            var projectSkill = _mapper.Map<ProjectSkill>(createDto);
            var created = await _projectSkillRepo.CreateProjectSkill(projectSkill);
            var resultDto = _mapper.Map<ProjectSkillDto>(created);

            return CreatedAtAction(nameof(GetById), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles ="client")]

        public async Task<IActionResult> Update(int id, [FromBody] ProjectSkillUpdateDto updateDto)
        {
            if (updateDto == null)
                return BadRequest();

            var projectSkill = new ProjectSkill
            {
                id = id,
                ProjectId = updateDto.ProjectId,
                SkillId = updateDto.SkillId
            };

            var updated = await _projectSkillRepo.UpdateAsync(projectSkill);
            if (updated == null)
                return NotFound();

            var resultDto = _mapper.Map<ProjectSkillDto>(updated);
            return Ok(resultDto);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles ="client")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _projectSkillRepo.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
