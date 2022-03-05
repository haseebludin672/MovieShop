using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;
//model data passing it to the view
//our controllers are very thin/lean
//most of your logic should come from other dependencies , services, Interface

namespace MovieShopMVC.Controllers
{
   
    public class HomeController : Controller
    {  
    private readonly IMovieService _MovieService;
    public HomeController(IMovieService movieService)
    {
            _MovieService = movieService;
    }
    
        [HttpGet]
        public IActionResult Index()
        {
            // var movieService = new MovieService();

            var movies = _MovieService.GetTop30GrossingMovies();

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