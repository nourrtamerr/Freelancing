using Freelancing.IRepositoryService;
using Freelancing.Models;
using Microsoft.EntityFrameworkCore;

namespace Freelancing.RepositoryService
{
    public class ReviewRepositoryService : IReviewRepositoryService
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepositoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Reviewee)
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task<List<Review>> GetReviewsByRevieweeIdAsync(string revieweeId)
        {
            return await _context.Reviews
                .Include(r => r.Reviewee)
                .Include(r => r.Reviewer)
                .Where(r => r.RevieweeId == revieweeId)
                .OrderByDescending(r => r.id)
                .ToListAsync();
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