using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService; 
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {// call mview service with details 
            //then pass movie data to view

            var movieDetails= _movieService.GetMovieDetails(Id);
            return View(movieDetails);
        }


    }
}
