using Microsoft.AspNetCore.Mvc;

namespace Lattency.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
