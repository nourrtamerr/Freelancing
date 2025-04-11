using AutoMapper;
using Freelancing.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    public class BansController : ControllerBase
    {
        private readonly IBanRepositoryService banService;
        private readonly IMapper mapper;

        public BansController(IBanRepositoryService banService, IMapper mapper)
        {
            this.banService = banService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BanDto>> GetBan(int id)
        {

            var ban = await banService.GetBanByIdAsync(id);
            if (ban == null)
            {
                return NotFound();
            }
            var banDto = mapper.Map<BanDto>(ban);
            return Ok(banDto);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<BanDto>>> GetBansByUserId(string userId)
        {
            var bans = await banService.GetBansByUserIdAsync(userId);
            if (bans == null || !bans.Any())
            {
                return NotFound();
            }
            var banDtos = mapper.Map<List<BanDto>>(bans);
            return Ok(banDtos);
        }

        [HttpPost]
        public async Task<ActionResult<BanDto>> CreateBan([FromBody] BanDto banDto)
        {
            if (banDto is null)
                return BadRequest("BanDto cannot be null");

            var ban = mapper.Map<Ban>(banDto);

            var createdBan = await banService.CreateBanAsync(ban);
            if (createdBan is null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create ban");

            var createdBanDto = mapper.Map<BanDto>(createdBan);

            return CreatedAtAction(
                nameof(GetBan),
                new { id = createdBanDto.Id },
                createdBanDto
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BanDto>> UpdateBan(int id, [FromBody] BanDto banDto)
        {
            if (banDto is null)
                return BadRequest("BanDto cannot be null");
            var existingBan = await banService.GetBanByIdAsync(id);
            if (existingBan == null)
            {
                return NotFound();
            }
            var updatedBan = mapper.Map<Ban>(banDto);
            updatedBan.Id = id;
            await banService.UpdateBanAsync(updatedBan);
            var updatedBanDto = mapper.Map<BanDto>(updatedBan);
            return Ok(updatedBanDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBan(int id)
        {
            var existingBan = await banService.GetBanByIdAsync(id);
            if (existingBan == null)
            {
                return NotFound();
            }
            await banService.DeleteBanAsync(id);
            return NoContent();
        }
    }
}
