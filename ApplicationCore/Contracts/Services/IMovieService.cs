using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        //have all the business logic methods relating to movies
        List<MovieCardModel> GetTop30GrossingMovies();
        
    }
}
