using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using LogicLayer.Models;
using LogicLayer.Services;

namespace Movie_wacth_list.Controllers
{
    public class UserListController : Controller
    {
        private readonly MovieService _movieService;
        private readonly UserService _userService;

        public UserListController(MovieService movieService, UserService userService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public IActionResult Index()
        {
            int userId = 2; 

            var checks = _userService.checkUserId(userId);
            if (checks == false)
            {
                return NotFound("User not found.");
            }
            var watches = _movieService.GetWatchList( userId);
           
            return View(watches.ToList());



        }
    }
}
