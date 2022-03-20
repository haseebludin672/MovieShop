using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {

            var userId = _currentUser.UserId;
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            return View(purchases);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUser.UserId;
            var favorites = await _userService.GetAllFavoritesForUser(userId);
            return View(favorites);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var userId = _currentUser.UserId;
            var reviews = await _userService.GetAllReviewsByUser(userId);
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel model)
        {
            var userId = _currentUser.UserId;
            var buy = await _userService.PurchaseMovie(model, userId);
            return Redirect($"~/Movies/Details/{model.MovieId}");
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(int movieId)
        {
            var userId = _currentUser.UserId;
            var favoriteMovie = await _userService.AddFavorite(new FavoriteRequestModel
            {
                UserId = userId,
                MovieId = movieId
            });
            return Redirect($"~/Movies/Details/{movieId}");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnfavoriteMovie(int id)
        {
            var userId = _currentUser.UserId;
            var unfavoriteMovie = await _userService.RemoveFavorite(new FavoriteRequestModel
            {
                MovieId = id,
                UserId = userId
            });
            return Redirect($"~/Movies/Details/{id}");
        }
        [HttpPost]
        public async Task<IActionResult> ReviewMovie(ReviewRequestModel model)
        {
            var userId = _currentUser.UserId;
            model.UserId = userId;
            var review = await _userService.AddMovieReview(model);
            return View($"~/Movies/Details/{model.MovieId}");
        }
    }
}
     
