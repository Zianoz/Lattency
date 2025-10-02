using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IPersonService _personService;

        public RegisterController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<ActionResult<Person>> CreatePerson([FromForm] PersonDTO dto, string confirmPassword)
        {
            if (dto.Password != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(dto);
            }

            var person = await _personService.CreatePersonAsync(dto);
            return Ok("Account Successfully Created!");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
