using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

                                    //1-4 purchases methods
        //1.purchase movie method
        public async Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = new Purchase
            {
                Id = purchaseRequest.Id,
                UserId = userId,
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime
            };

            var createdPurchase = await _userRepository.AddPurchase(purchase);
            return createdPurchase.Id;
        }

        //2.checking method wether a movie is purchased or not
        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var favoriteExist = await _userRepository.UserPurchaseExist(purchaseRequest.Id, userId);
            return favoriteExist;
        }

        //3.getall purchesed movie by a user
        public async Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            var purchases = await _userRepository.GetAllPurchasesFromUser(id);
            var purchaseList = new List<MovieCardModel>();

            // mapping entities data in to models data
            foreach (var purchase in purchases)
                purchaseList.Add(new MovieCardModel
                {
                    Id = purchase.Movie.Id,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                });

            return purchaseList;
        }
        //4.get PurchaseDetails
       public async Task<PurchaseRequestModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchse = await  _userRepository.GetUserPurchase(userId, movieId);
            var purchaseDetails = new PurchaseRequestModel
            {
                Id = userId,
                MovieId = movieId,
                UserId = userId,
                PurchaseNumber = purchse.PurchaseNumber,
                PurchaseDateTime = purchse.PurchaseDateTime,
                TotalPrice = purchse.TotalPrice,

            };

            return purchaseDetails;
        }



                                // 5-8 Favorits method
        //5.add favorites
        public async Task<int> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = new Favorite
            {
                Id = favoriteRequest.Id,
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };

            var createdFavorite = await _userRepository.AddFavorite(favorite);
            return createdFavorite.Id;
        }
        //6.Remove favorite
        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _userRepository.RemoveFavorite(favoriteRequest.Id, favoriteRequest.UserId, favoriteRequest.MovieId);
        }
        //7.Exist favorite
        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var favoriteExist = await _userRepository.UserFavoriteExist(id, movieId);
            return favoriteExist;
        }
        //8.GetAll Favorite movies of a user
        public async Task<List<MovieCardModel>> GetAllFavoritesForUser(int id)
        {
            var favorites = await _userRepository.GetAllFavoritesFromUser(id);
            var favoriteList = new List<MovieCardModel>();

            // mapping entities data in to models data
            foreach (var favorite in favorites)
                favoriteList.Add(new MovieCardModel
                {
                    Id = favorite.Movie.Id,
                    Title = favorite.Movie.Title,
                    PosterUrl = favorite.Movie.PosterUrl
                });

            return favoriteList;
        }
        


                             //9-12 Review methods
        //9.Add review method 
        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };

            var userRview = await _userRepository.AddReview(review);
        }
        //10. UpdateMovieReview
        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };

            var createdReview = await _userRepository.UpdateReview(review);
        }

        //11.Delete Movie review
        public async Task DeleteMovieReview(int userId, int movieId)
        {
            await _userRepository.RemoveReview(userId, movieId);
        }

        //12. Get All reviews by users
        public async Task<List<MovieCardModel>> GetAllReviewsByUser(int id)
        {
            var reviews = await _userRepository.GetAllReviewsFromUser(id);
            var reviewList = new List<MovieCardModel>();

            // mapping entities data in to models data
            foreach (var review in reviews)
                reviewList.Add(new MovieCardModel
                {
                    Id = review.Movie.Id,
                    Title = review.Movie.Title,
                    PosterUrl = review.Movie.PosterUrl
                });

            return reviewList;
        }
    }
}


