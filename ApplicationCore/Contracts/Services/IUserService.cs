using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
      //  Task<RegisterModel> GetUserDetails(int id);
        Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<List<PurchaseModel>> GetAllPurchasesForUser(int id);
        Task<PurchaseModel> GetPurchasesDetails(int userId, int movieId);

        Task<int> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<int> RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<List<FavoriteModel>> GetAllFavoritesForUser(int id);

        Task<ReviewModel> AddMovieReview(ReviewRequestModel reviewRequest);
        Task<ReviewModel> UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task<string> DeleteMovieReview(int userId, int movieId);
        Task<List<ReviewModel>> GetAllReviewsByUser(int id);

    }
}
