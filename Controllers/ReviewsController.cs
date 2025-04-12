using AutoMapper;
using Freelancing.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController:ControllerBase
    {
        private readonly IReviewRepositoryService reviewService;
        private readonly IMapper mapper;

        public ReviewsController(IReviewRepositoryService reviewService, IMapper mapper)
        {
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            var reviewDto = mapper.Map<ReviewDto>(review);
            return Ok(reviewDto);
        }

        [HttpGet("reviewee/{revieweeId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsByRevieweeId(string revieweeId)
        {
            var reviews = await reviewService.GetReviewsByRevieweeIdAsync(revieweeId);
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }
            var reviewDtos = mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("reviewer/{reviewerId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsByReviewerId(string reviewerId) {

            var reviews = await reviewService.GetReviewsByReviewerIdAsync(reviewerId);
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound();
            }
            var reviewDtos = mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> CreateReview([FromBody] ReviewDto reviewDto)
        {
            if (reviewDto == null)
            {
                return BadRequest();
            }
            var review = mapper.Map<Review>(reviewDto);
            var createdReview = await reviewService.CreateReviewAsync(review);
            var createdReviewDto = mapper.Map<ReviewDto>(createdReview);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.id }, createdReviewDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
            {
                return BadRequest();
            }
            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            mapper.Map(reviewDto, review);
            await reviewService.UpdateReviewAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            await reviewService.DeleteReviewAsync(id);
            return NoContent();
        }
    }
}
