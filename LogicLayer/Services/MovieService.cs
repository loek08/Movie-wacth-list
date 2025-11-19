using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Interfaces;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }
        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetMovies();
        }

        public IEnumerable<MovieToWatch> GetWatchList(int userId)
        {
            if (userId == 0)
            {
                return Enumerable.Empty<MovieToWatch>();
            }
            return _movieRepository.ToWatches();

        }
    }
}
