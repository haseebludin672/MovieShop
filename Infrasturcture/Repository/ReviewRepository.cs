using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository : EfRepository<Review>, IReviewRepository
{
    public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Review> GetReviewByUser(int movieId, int userId)
    {
        var review = await _dbContext.Reviews.Include(r => r.Movie)
            .FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
        return review;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsByUser(int id)
    {
        var reviews = await _dbContext.Reviews.Include(r => r.Movie)
            .Where(r => r.UserId == id).ToListAsync();
        return reviews;
    }
}