using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.JsonPatch;

namespace Lattency.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAPIController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuAPIController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("CreateMenu")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Menu>> CreateMenuAsync(MenuCreationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Menu cannot be null");
            }

            var createdMenu = await _menuService.CreateMenuAsync(dto);

            return CreatedAtAction(nameof(GetMenuByIdAsync), new { id = createdMenu.Id }, createdMenu);
        }

        [HttpPost("AddDish")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dish>> CreateDishtoMenuIdAsync(int menuId, DishCreationDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dish cannot be null");
            }
            var createdDish = await _menuService.CreateDishtoMenuIdAsync(menuId, dto);
            return Ok(createdDish);
        }

        [HttpPut("UpdateDish")]
        public async Task<ActionResult<Dish>> UpdateDishInMenuAsync(int menuId, int dishId, UpdateDishDTO dto)
        {
            var updatedDish = await _menuService.UpdateDishInMenuAsync(menuId, dishId, dto);

            return Ok(updatedDish);
        }

        [HttpPost("GetAllMenus")]
        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            var menus = await _menuService.GetAllMenusAsync();
            return menus;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenuByIdAsync(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return Ok(menu);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>DeleteMenuAsync(int id)
        {
            var menu = await _menuService.DeleteMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return NoContent();
        }
    }
}
