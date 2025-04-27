using AutoMapper;
using Freelancing.DTOs;
using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class ReviewRepositoryService : IReviewRepositoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewRepositoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Reviewee)
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        //public async Task<List<Review>> GetReviewsByRevieweeIdAsync(string revieweeId)
        //{
        //    return await _context.Reviews
        //        .Include(r => r.Reviewee)
        //        .Include(r => r.Reviewer)
        //        .Where(r => r.RevieweeId == revieweeId)
        //        .OrderByDescending(r => r.id)
        //        .ToListAsync();
        //}

        public async Task<List<GetReviewByRevieweeIdDto>> GetReviewsByRevieweeIdAsync(string revieweeId)
        {
           var reviews= await _context.Reviews
                .Include(r => r.Reviewee)
                .Include(r => r.Reviewer)
                .Include(r=>r.Project)
                .Where(r => r.RevieweeId == revieweeId)
                .OrderByDescending(r => r.id)
                .ToListAsync();
            var ReviewsDto = _mapper.Map<List<GetReviewByRevieweeIdDto>>(reviews);

            return ReviewsDto;
        }

        public async Task<List<Review>> GetReviewsByReviewerIdAsync(string reviewerId)
        {
            return await _context.Reviews
                .Include(r => r.Reviewee)
                .Include(r => r.Reviewer)
                .Where(r => r.ReviewerId == reviewerId)
                .OrderByDescending(r => r.id)
                .ToListAsync();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
         
            if((review.RevieweeId==review.Project.FreelancerId && review.ReviewerId==review.Project.ClientId) ||
                (review.RevieweeId == review.Project.ClientId && review.ReviewerId == review.Project.FreelancerId))
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}