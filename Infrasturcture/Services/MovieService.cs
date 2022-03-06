using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCardModel> GetTop30GrossingMovies()
        {
            var movies = _movieRepository.GetTop30RevenueMovies();
            var mvoieCards= new List<MovieCardModel>();

            //mapping entities data into the models data
            foreach (var movie in movies)
            {
                mvoieCards.Add(new MovieCardModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title,
                });
            }
            return mvoieCards;
              
        }
           

    }

}

