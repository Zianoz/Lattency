using Microsoft.AspNetCore.Mvc;

namespace Lattency.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
