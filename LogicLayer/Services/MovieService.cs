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

        public IReadOnlyList<MovieToWatch> GetWatchList(int userId)
        {
            if (userId < 0) throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be non-negative.");
            return _movieRepository.ToWatches(userId);
        }

        public void AddMovieToWatchList(int userId, int movieId)
        {
            if (userId < 0) throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be non-negative.");
            if (movieId < 0) throw new ArgumentOutOfRangeException(nameof(movieId), "Movie ID must be non-negative.");

            var user = new User();
            // Fix: Pass required parameters to HasMovieToWatch
            if (user.HasMovieToWatch(movieId, userId, this) == false) throw new InvalidOperationException("Movie bestaat al");



            _movieRepository.AddMovieToWatchList(userId, movieId);
        }

        public void RemoveMovieFromWatchList(int userId, int movieId)
        {
            if (userId < 0) throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be non-negative.");
            if (movieId < 0) throw new ArgumentOutOfRangeException(nameof(movieId), "Movie ID must be non-negative.");
            _movieRepository.RemoveMovieFromWatchList(userId, movieId);
        }


    }
}
