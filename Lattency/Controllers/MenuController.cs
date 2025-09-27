using Lattency.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Threading.Tasks;

namespace Lattency.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return View(menus);
        }
    }
}
