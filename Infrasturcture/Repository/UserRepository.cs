using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Include(u=> u.UserRoles).FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }


        //Pucrhases
        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase;
        }
        public async Task<Purchase> GetUserPurchase(int userId, int movieId)
        {
            var purchaseDetail = await _dbContext.Purchases
                .FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
            return purchaseDetail;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesFromUser(int userId)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).ToListAsync();
            return purchases;
        }

        public async Task<bool> UserPurchaseExist(int purchaseId, int userId)
        {
            var matchPurchase = await _dbContext.Purchases.FirstOrDefaultAsync(f => f.UserId == userId && f.Id == purchaseId);
            if (matchPurchase != null)
            {
                return true;
            }
            return false;
        }

        //Favorites

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }
        public async Task<IEnumerable<Favorite>> GetAllFavoritesFromUser(int userId)
        {
            var favorites = await _dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Movie).ToListAsync();
            return favorites;
        }

        public async Task<bool> UserFavoriteExist(int userId, int movieId)
        {
            var matchFavorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
            if (matchFavorite != null)
            {
                return true;
            }
            return false;
        }

        public async Task RemoveFavorite(int favoriteId, int userId, int movieId)
        {
            var favoriteToRemove = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId && f.Id == favoriteId);
            if (favoriteToRemove != null)
            {
                _dbContext.Favorites.Remove(favoriteToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        //Reviews
        public async Task<IEnumerable<Review>> GetAllReviewsFromUser(int userId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.UserId == userId).Include(r => r.Movie).ToListAsync();
            return reviews;
        }

        public async Task<Review> AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review> UpdateReview(Review review)
        {
            _dbContext.Reviews.Update(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task RemoveReview(int userId, int movieId)
        {
            var reviewToRemove = await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            if (reviewToRemove != null)
            {
                _dbContext.Reviews.Remove(reviewToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

      
    }
}