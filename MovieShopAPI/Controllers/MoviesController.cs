using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    //attribute routing
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // in REST pattern we dont specify the http erbs in the url
        // http://movieshop.com/api/movies2  => json data
        // http://movieshop.com/movies/details/2  => View
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
            
        }
       
        [HttpGet]
        public async Task<IActionResult> GetMoviesByPagination(int genreId, int pageSize, int pageNumber) 
        {
            var MoviesbyPagination = await _movieService.GetMoviesByGenrePagination(genreId, pageSize = 10,  pageNumber = 1);
                if (MoviesbyPagination == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: " });
            }
            return Ok(MoviesbyPagination);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            //return the data in json format
            //HTTP status code , 200 ok, 400 error
            if(movie == null)
            {
                return NotFound(new {error =$"Movie Not Found for id: {id}"});
            }
            return Ok(movie);
            //in old .net for JSON serlization se used JSON.NET library  
        }
        [Route("top-rated")]
        [HttpGet]
        public async Task<IActionResult> GetTop30Movies()
        {
            var top30Movies = await _movieService.GetTop30GrossingMovies();
            if (top30Movies == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: " });
            }
            return Ok(top30Movies);
        }
        [Route("Movies/top-grossing")]
        [HttpGet]
        public async Task<IActionResult> GetTopGrossingMovie()
        {
            var topGrossingMovie = await _movieService.GetTop30GrossingMovies();
            if (topGrossingMovie == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: " });
            }
            return Ok(topGrossingMovie);
        }
        [Route("genres/{genreId:int}")]
        [HttpGet]
        public async Task<IActionResult> MoviesByGenre(int id, int pageSize = 30, int pageNumber = 1)
        { var movieGenre= await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            if (movieGenre == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: " });
            }
            return Ok(movieGenre);
        }
        [Route("{id:int}/review")]
        [HttpGet]
        public async Task<IActionResult> MovieReview(int genreId, int pageSize = 30, int pageNumber = 1) 
        {
            var movieReview = await _movieService.GetMoviesByGenrePagination(genreId, pageSize, pageNumber);
            if (movieReview == null)
            {
                return NotFound(new { error = $"Movie Not Found for id: " });
            }
            return Ok(movieReview);
        }

    }
}
