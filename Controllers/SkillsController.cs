﻿using AutoMapper;
using Freelancing.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService ?? throw new ArgumentNullException(nameof(skillService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            var skillDtos = _mapper.Map<IEnumerable<SkillDto>>(skills);
            return Ok(skillDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDto>> GetSkillById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid skill ID");

            var skill = await _skillService.GetSkillByIDAsync(id);
            if (skill == null)
                return NotFound($"Skill with ID {id} not found");

            return Ok(_mapper.Map<SkillDto>(skill));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] SkillDto skillCreateDto)
        {
            if (skillCreateDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var skill = _mapper.Map<Skill>(skillCreateDto);
            var createdSkill = await _skillService.CreateSkillAsync(skill);

            var skillDto = _mapper.Map<SkillDto>(createdSkill);
            return CreatedAtAction(nameof(GetSkillById), new { id = skillDto.Id }, skillDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDto>> UpdateSkill(int id, [FromBody] SkillDto skillUpdateDto)
        {
            if (skillUpdateDto == null || id != skillUpdateDto.Id || !ModelState.IsValid)
                return BadRequest("Invalid skill data or ID mismatch");

            var skill = _mapper.Map<Skill>(skillUpdateDto);
            var updatedSkill = await _skillService.UpdateSkillAsync(skill);

            if (updatedSkill == null)
                return NotFound($"Skill with ID {id} not found");

            return Ok(_mapper.Map<SkillDto>(updatedSkill));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid skill ID");

            var result = await _skillService.DeleteSkillAsync(id);
            if (!result)
                return NotFound($"Skill with ID {id} not found");

            return NoContent();
        }
    }
}