using Microsoft.AspNetCore.Mvc;

namespace VenueBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
