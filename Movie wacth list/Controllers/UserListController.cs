using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using LogicLayer.Models;
using LogicLayer.Services;

namespace Movie_wacth_list.Controllers
{
    public class UserListController : Controller
    {
        private readonly MovieService _movieService;

        public UserListController(MovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }
        public IActionResult Index()
        {
            int userId = 2; // Replace with actual user ID retrieval logic
            var watches = _movieService.GetWatchList( userId);
            return View(watches.ToList());



        }
    }
}
