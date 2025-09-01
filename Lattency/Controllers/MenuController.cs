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

        [HttpPost]
        public async Task<ActionResult<Menu>> CreateMenuAsync([FromBody] Menu menu)
        {
            if (menu == null)
            {
                return BadRequest("Menu cannot be null");
            }

            var createdMenu = await _menuService.CreateMenuAsync(menu);

            return CreatedAtAction(nameof(GetMenuByIdAsync), new { id = createdMenu.Id }, createdMenu);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenuByIdAsync(int id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            if (menu == null) return NotFound();
            return Ok(menu);
        }
    }
}
