using Microsoft.AspNetCore.Mvc;

namespace FrontEndMVC.Controllers
{
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
