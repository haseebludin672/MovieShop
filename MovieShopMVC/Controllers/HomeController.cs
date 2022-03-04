using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller


    {[HttpGet]
        public IActionResult Index()
        {
            var movieService = new MovieService();
            //model data passing it to the view
            var movies = movieService.GetTop30GrossingMovies();
            //our controllers are very thin/lean
            //most of your logic should come from other dependencies , services
            //Interfaces
            //void method (int x, ImovieService service);
            //method (20, movieservice);
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}