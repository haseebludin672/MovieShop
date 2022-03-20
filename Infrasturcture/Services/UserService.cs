using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        //private readonly IUserService _userService; 
        private readonly IReviewRepository _reviewRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;


        public UserService(IUserService userservice,  IReviewRepository reviewRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
        {
            //_userService = userservice;
            _reviewRepository = reviewRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
        }
        //User Details
        //public async Task<RegisterModel> GetUserDetails(int id)
        //{
        //    var user = await _userService.GetUserDetails(id);
        //    var userDetails = new RegisterModel
        //    {
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        DateOfBirth = user.DateOfBirth
        //    };
        //    return userDetails;
        //}


        //1-4 purchases methods
        //1.purchase movie method
        public async Task<int> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // check if user already purchased or not
            var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(purchaseRequest.MovieId, userId);
            if (purchasedByUser != null)
            {
                throw new Exception("Already bought");
            }

            var newPurchased = new Purchase
            {
                UserId = userId,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId,
                TotalPrice = purchaseRequest.TotalPrice,
                PurchaseNumber = purchaseRequest.PurchaseNumber
            };
            var createdPurchased = await _purchaseRepository.Add(newPurchased);
            return createdPurchased.Id;
        }

        //     //2.checking method wether a movie is purchased or not
        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(purchaseRequest.MovieId, userId);
            if (purchasedByUser != null)
            {
                return true;
            }

            return false;
        }

        //3.getall purchesed movie by a user
        public async Task<List<PurchaseModel>> GetAllPurchasesForUser(int id)
        {
            var Purchases = await _purchaseRepository.GetAllPurchasesForUser(id);
            var ListOfPurchase = new List<PurchaseModel>();
            foreach (var p in Purchases)
            {
                var PurchasesDetails = new PurchaseModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    PurchaseDateTime = p.PurchaseDateTime,
                    TotalPrice = p.TotalPrice,
                    PurchaseNumber = p.PurchaseNumbers,
                };
                PurchasesDetails.Movie = new MovieCardModel
                {
                    Id = p.MovieId,
                    PosterUrl = p.Movie.PosterUrl,
                    Title = p.Movie.Title
                };

                ListOfPurchase.Add(PurchasesDetails);
            }

            return ListOfPurchase;
        }
        // //4.get PurchaseDetails
        public async Task<PurchaseModel> GetPurchasesDetails(int userId, int movieId)
        {
            var purchasedByUser = await _purchaseRepository.GetPurchaseByUser(movieId, userId);
            if (purchasedByUser == null)
            {
                return null;
            }
            return new PurchaseModel
            {
                Id = purchasedByUser.Id,
                UserId = purchasedByUser.UserId,
                PurchaseDateTime = purchasedByUser.PurchaseDateTime,
                TotalPrice = purchasedByUser.TotalPrice,
                PurchaseNumber = purchasedByUser.PurchaseNumbers,
                Movie = new MovieCardModel
                {
                    Id = purchasedByUser.Movie.Id,
                    PosterUrl = purchasedByUser.Movie.PosterUrl,
                    Title = purchasedByUser.Movie.Title
                }
            };
        }



        //                         // 5-8 Favorits method
        // //5.add favorites
        public async Task<int> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _favoriteRepository.GetFavoriteByUser(favoriteRequest);
            if (favorite != null)
            {
                throw new Exception("already favorited");
            }

            var createdFavorite = await _favoriteRepository.Add(new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            });
            return createdFavorite.Id;
        }
        //6.Remove favorite
        public async Task<int> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _favoriteRepository.GetFavoriteByUser(favoriteRequest);
            if (favorite == null)
            {
                throw new Exception("not have that favorite");
            }

            var removedFavorite = await _favoriteRepository.Delete(favorite);
            return removedFavorite.Id;
        }
        //7.Exist favorite
        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var favorite =
           await _favoriteRepository.GetFavoriteByUser(new FavoriteRequestModel { MovieId = movieId, UserId = id });
            if (favorite != null)
            {
                return true;
            }

            return false;
        }
        //8.GetAll Favorite movies of a user
        public async Task<List<FavoriteModel>> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetAllFavoriteByUser(id);
            var newFavorites = new List<FavoriteModel>();
            foreach (var f in favorites)
            {
                newFavorites.Add(new FavoriteModel
                {
                    Id = f.Id,
                    MovieId = f.MovieId,
                    UserId = f.UserId,
                    Movie = new MovieCardModel
                    {
                        Id = f.Movie.Id,
                        PosterUrl = f.Movie.PosterUrl,
                        Title = f.Movie.Title,
                    }
                });

            }

            return newFavorites;
        }



        //                      //9-12 Review methods
        // //9.Add review method 
        public async Task<ReviewModel> AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetReviewByUser(reviewRequest.MovieId, reviewRequest.UserId);
            if (review != null)
            {
                throw new Exception("already reviewed");
            }

            var createdReview = await _reviewRepository.Add(new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = reviewRequest.UserId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            });
            var reviewModel = new ReviewModel
            {
                Title = createdReview.Movie.Title,
                PosterURL = createdReview.Movie.PosterUrl,
                MovieId = createdReview.MovieId,
                Rating = createdReview.Rating,
                ReviewText = createdReview.ReviewText,
                UserId = createdReview.UserId
            };
            return reviewModel;
        }
        //10.Update Movie Review
        public async Task<ReviewModel> UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var review = await _reviewRepository.GetReviewByUser(reviewRequest.MovieId, reviewRequest.UserId);
            if (review == null)
            {
                throw new Exception("not reviewed yet");
            }

            var updatedReview = await _reviewRepository.Update(new Review
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            });
            return new ReviewModel
            {
                MovieId = updatedReview.MovieId,
                Title = updatedReview.Movie.Title,
                PosterURL = updatedReview.Movie.PosterUrl,
                Rating = updatedReview.Rating,
                ReviewText = updatedReview.ReviewText,
                UserId = updatedReview.UserId,
            };
        }

        //11.Delete Movie review
        public async Task<string> DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.GetReviewByUser(movieId, userId);
            if (review == null)
            {
                throw new Exception("not reviewed yet");
            }

            var createdReview = await _reviewRepository.Delete(review);
            return "deleted";
        }

            //12. Get All reviews by users
            public async Task<List<ReviewModel>> GetAllReviewsByUser(int id)
            {
            var reviews = await _reviewRepository.GetAllReviewsByUser(id);
            var reviewsModel = new List<ReviewModel>();
            foreach (var review in reviews)
            {
                reviewsModel.Add(new ReviewModel
                {
                    MovieId = review.MovieId,
                    PosterURL = review.Movie.PosterUrl,
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText,
                    Title = review.Movie.Title
                });
            }

            return reviewsModel;
            }
    }
    }


