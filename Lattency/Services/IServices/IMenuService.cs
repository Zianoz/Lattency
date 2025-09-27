using Lattency.DTOs;
using Lattency.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Services.IServices
{
    public interface IMenuService
    {
        Task<Menu> CreateMenuAsync(MenuCreationDTO dto);
        Task<Menu> GetMenuByIdAsync(int id);
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<IActionResult> DeleteMenuByIdAsync(int id);
        Task<ActionResult<Dish>> CreateDishtoMenuIdAsync(int menuId, DishCreationDTO dto);
    }
}
