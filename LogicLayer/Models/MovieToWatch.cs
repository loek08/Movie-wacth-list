using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Models
{
    public class MovieToWatch
    {
        public string MovieName { get; private set; }
        public int MovieId { get; private set; }
        public string Status { get; private set; }
        //public List() { }
        public MovieToWatch(string movieName, int movieId, string status) {
            MovieName = movieName;
            MovieId = movieId;
            Status = status;
        }


        public bool CheckIfMovieExistInUserList(int userId, int movieId, IMovieRepository repository)
        {
            while (repository.ToWatches(userId).Any(m => m.MovieId == movieId))
            {
                return true;
            }
            return false;
        }
    }
}
