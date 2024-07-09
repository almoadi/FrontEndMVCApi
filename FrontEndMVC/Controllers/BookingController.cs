using Microsoft.AspNetCore.Mvc;

namespace FrontEndMVC.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
