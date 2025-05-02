using AutoMapper;
using Freelancing.DTOs;
using Freelancing.Filters;
using Freelancing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Freelancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReviewsController:ControllerBase
    {
        private readonly IReviewRepositoryService reviewService;
        private readonly IMapper mapper;
        private readonly INotificationRepositoryService _notifications;

		public ReviewsController(INotificationRepositoryService notifications,IReviewRepositoryService reviewService, IMapper mapper)
        {
            this.reviewService = reviewService;
            this.mapper = mapper;
            _notifications = notifications;

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
        public async Task<ActionResult<List<GetReviewByRevieweeIdDto>>> GetReviewsByRevieweeId(string revieweeId)
        {
            var reviews = await reviewService.GetReviewsByRevieweeIdAsync(revieweeId);
            if (reviews == null )
            {
                return NotFound();
            }
            var reviewDtos = mapper.Map<List<GetReviewByRevieweeIdDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("reviewer/{reviewerId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsByReviewerId(string reviewerId) {

            var reviews = await reviewService.GetReviewsByReviewerIdAsync(reviewerId);
            if (reviews == null )
            {
                return NotFound();
            }
            var reviewDtos = mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }
		[HttpGet("project/{projectId}")]
		public async Task<ActionResult<List<ReviewDto>>> GetReviewsByProjectId(int projectId)
		{

			var reviews = await reviewService.GetReviewsByProjectIdAsync(projectId);
			if (reviews == null )
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
            review.ProjectId = reviewDto.projectId??0;
            var createdReview = await reviewService.CreateReviewAsync(review);
            await _notifications.CreateNotificationAsync(new()
            {
                isRead = false,
                Message = $"you got reviewed by userid {review.ReviewerId}",
                UserId = review.RevieweeId
            });
			var createdReviewDto = mapper.Map<ReviewDto>(createdReview);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.id }, createdReviewDto);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ReviewAuthorizationFilter))]
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
            //mapper.Map(reviewDto, review);
            review.Comment = reviewDto.Comment;
            review.Rating = reviewDto.Rating;

			await reviewService.UpdateReviewAsync(review);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ReviewAuthorizationFilter))]
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
