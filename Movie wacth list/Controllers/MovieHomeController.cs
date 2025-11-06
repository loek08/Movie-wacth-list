using Microsoft.AspNetCore.Mvc;

namespace Movie_wacth_list.Controllers
{
    public class MovieHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
