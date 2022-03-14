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
        public UserController(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        //used a cleaner way of the commited code created Icurrent user + current user interface and class in MOVIEShopeMVC solutuion Explorer
        //first thing is whether user is loged in or not

        //var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
        //if (isAuthenticated)
        //{
        //    //get the user id from cookies/claims
        //    var userId =Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    //send this UserId to UserService and get the movies user purchased from purchase table 



        //send the user to databse to get all the movies user purchased  
        // Cookie based authentication


        //now using filter on our code to check if user is logged in or not a short code and easy
        [HttpGet]
       
        public async Task<IActionResult> Purchases()
        {
                var UserId = _currentUser.UserId;
            
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
        public async Task<IActionResult> ReviewMovie()
        {
            return View();
        }
    }
}
     
