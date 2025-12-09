using LogicLayer.Models;
using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Movie_wacth_list.Controllers
{
    public class MovieHomeController : Controller
    {
        private readonly MovieService _movieService;

        public MovieHomeController(MovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }
        public IActionResult Index()
        {
            var movies = _movieService.GetAllMovies() ?? Enumerable.Empty<Movie>();
            return View(movies.ToList());
        }

        public IActionResult Details(int id)
        {

            var movie = _movieService.GetAllMovies() ?.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();


            return View(movie);
        }
        public IActionResult AddToWatchList(int movieId)
        {
            
            int userId = 2; 
            try
            {
                var checks = _movieService.CheckMovieExists(movieId);
                if (checks == true)
                {
                   return BadRequest("Movie already in watchlist.");
                }
                _movieService.AddMovieToWatchList(userId, movieId);

                return RedirectToAction("Index", "UserList");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        public IActionResult RemoveFromWatchList(int movieId)
        {
            int userId = 2; 
            try
            {
                _movieService.RemoveMovieFromWatchList(userId, movieId);
                return RedirectToAction("Index", "UserList");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
