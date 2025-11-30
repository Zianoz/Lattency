using Lattency.DTOs;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lattency.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMenuService _menuService;
        private readonly ICafeTableService _cafeTableService;
        private readonly IPersonService _personService;

        public AdminController(
            IBookingService bookingService,
            IMenuService menuService,
            ICafeTableService cafeTableService,
            IPersonService personService)
        {
            _bookingService = bookingService;
            _menuService = menuService;
            _cafeTableService = cafeTableService;
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Bookings Management

        public async Task<IActionResult> Bookings()
        {
            var bookings = await _bookingService.GetAllPersonBookingsAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            TempData["SuccessMessage"] = "Booking deleted successfully!";
            return RedirectToAction(nameof(Bookings));
        }

        #endregion

        #region Menu Management

        public async Task<IActionResult> Menus()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return View(menus);
        }

        [HttpGet]
        public IActionResult CreateMenu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu(MenuCreationDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _menuService.CreateMenuAsync(dto);
            TempData["SuccessMessage"] = "Menu created successfully!";
            return RedirectToAction(nameof(Menus));
        }

        [HttpGet]
        public async Task<IActionResult> MenuDetails(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null)
                return NotFound();
            
            return View(menu);
        }

        [HttpGet]
        public IActionResult CreateDish(int menuId)
        {
            ViewBag.MenuId = menuId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(int menuId, DishCreationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MenuId = menuId;
                return View(dto);
            }

            await _menuService.CreateDishtoMenuIdAsync(menuId, dto);
            TempData["SuccessMessage"] = "Dish added successfully!";
            return RedirectToAction(nameof(MenuDetails), new { id = menuId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            await _menuService.DeleteMenuByIdAsync(id);
            TempData["SuccessMessage"] = "Menu deleted successfully!";
            return RedirectToAction(nameof(Menus));
        }

        #endregion

        #region Tables Management

        public async Task<IActionResult> Tables()
        {
            var tables = await _cafeTableService.GetAllCafeTablesAsync();
            return View(tables);
        }

        [HttpGet]
        public IActionResult CreateTable()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable(CafeTableDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _cafeTableService.CreateCafeTableAsync(dto);
            TempData["SuccessMessage"] = "Table created successfully!";
            return RedirectToAction(nameof(Tables));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _cafeTableService.DeleteCafeTableAsync(id);
            if (result)
                TempData["SuccessMessage"] = "Table deleted successfully!";
            else
                TempData["ErrorMessage"] = "Table not found!";
            
            return RedirectToAction(nameof(Tables));
        }

        #endregion

        #region Users Management

        public async Task<IActionResult> Users()
        {
            var users = await _personService.GetAllPersonsAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _personService.DeletePersonAsync(id);
            if (result)
                TempData["SuccessMessage"] = "User deleted successfully!";
            else
                TempData["ErrorMessage"] = "User not found!";
            
            return RedirectToAction(nameof(Users));
        }

        #endregion
    }
}
