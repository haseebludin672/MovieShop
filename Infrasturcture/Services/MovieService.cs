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
        public List<MovieCardModel> GetTop30GrossingMovies()
        {// call movie repository which is gonna call database with the help of E.F
            //now we dont have DB so use damy data
            var movies = new List<MovieCardModel>()
            {
                 new MovieCardModel { Id =1, PosterUrl ="https://image.tmdb.org/t/p/original//s3TBrRGB1iav7gFOCNx3H31MoES.jpg", Title ="Inception"},
                new MovieCardModel { Id =2, PosterUrl ="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", Title ="Interstellar"},
                new MovieCardModel { Id =3, PosterUrl ="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg", Title =""},
                new MovieCardModel { Id =4, PosterUrl ="", Title ="The Dark Knight"}
               
            };
            return movies;

        }

    }
}
