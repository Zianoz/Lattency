using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Lattency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("CreateMenu")]
        public async Task<ActionResult<Menu>> CreateMenuAsync([FromBody] MenuCreationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Menu cannot be null");
            }

            var createdMenu = await _menuService.CreateMenuAsync(dto);

            return Ok(createdMenu);
        }

        [HttpPost("AddDish")]
        public async Task<ActionResult<Dish>> CreateDishtoMenuIdAsync(int menuId, [FromBody] DishCreationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dish cannot be null");
            }
            var createdDish = await _menuService.CreateDishtoMenuIdAsync(menuId, dto);
            return Ok(createdDish);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenuByIdAsync(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return Ok(menu);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteMenuAsync(int id)
        {
            var menu = await _menuService.DeleteMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return NoContent();
        }
    }
}
