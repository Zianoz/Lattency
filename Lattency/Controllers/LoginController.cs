using Lattency.DTOs;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Controllers
{
    public class LoginController : Controller
    {
        private readonly IPersonService _personService;

        public LoginController(IPersonService personService)
        {
            _personService = personService;
        }

        // Controller for login
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _personService.LoginAsync(dto);
            if (token == null)
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View("Index"); // reload view with error
            }

            // Store token in session or cookie
            HttpContext.Session.SetString("JwtToken", token);

            return RedirectToAction("Index", "Home");
        }

        // Show the login page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


    }
}
