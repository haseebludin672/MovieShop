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
            var UserId = _currentUser.UserId;
            var purchaseList = _userService.GetAllPurchasesForUser(UserId);
            return View(purchaseList);

        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUser.UserId;
            var favoritesList = _userService.GetAllFavoritesForUser(userId);
            return View(favoritesList);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            var userId= _currentUser.UserId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchaseRequestM)
        {
            var userId = _currentUser.UserId;
            var purchased =  _userService.PurchaseMovie(purchaseRequestM, userId);
            return View();
        }

        public async Task<IActionResult> FavoriteMovie()
        {
            var userId = _currentUser.UserId;
            return View();
        }
        public async Task<IActionResult> ReviewMovie()
        {
          // var userId = _currentUser.UserId;
           //var addreview =  _userService.AddMovieReview(reviewRequest);
           return View();
        }
    }
}
     
