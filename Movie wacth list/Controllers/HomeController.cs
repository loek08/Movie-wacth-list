using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie_wacth_list.Models;
using LogicLayer.Models;
using LogicLayer.Services;
using System.Linq;

namespace Movie_wacth_list.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public IActionResult Index()
        {
            
            var users = _userService.GetAllUsers() ?? Enumerable.Empty<User>();
            return View(users.ToList());
            
        }




    }
}
