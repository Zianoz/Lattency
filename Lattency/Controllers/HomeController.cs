using Microsoft.AspNetCore.Mvc;

namespace Lattency.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
