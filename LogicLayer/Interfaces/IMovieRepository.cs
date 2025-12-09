using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Interfaces
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IReadOnlyList<MovieToWatch> ToWatches(int userId);
        void AddMovieToWatchList(int userId, int movieId);
        void RemoveMovieFromWatchList(int userId, int movieId);

    }
}
