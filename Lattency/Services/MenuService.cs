using Lattency.DTOs;
using Lattency.Mappers.DTOMappers;
using Lattency.Models;
using Lattency.Repositories.IRepositories;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<Menu> CreateMenuAsync (MenuCreationDTO dto)
        {
            var menu = new Menu
            {
                Id = dto.Id,
                Name = dto.Name,
                Dishes = new List<Dish>()
            };

            await _menuRepository.AddAsync(menu);
            await _menuRepository.SaveChangesAsync();
            return menu;
        }

        public async Task<ActionResult<Dish>> CreateDishtoMenuIdAsync(int menuId, DishCreationDTO dto)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
            {
                return new NotFoundResult();
            }

            var dish = new Dish
            {
                Id = dto.Id,
                DishName = dto.DishName,
                Description = dto.Description,
                Price = dto.Price,
                IsPopular = dto.IsPopular,
                ImageURL = dto.ImageURL,
                MenuId = menuId
            };
            menu.Dishes.Add(dish);
            await _menuRepository.UpdateAsync(menu);
            await _menuRepository.SaveChangesAsync();
            return dish;
        }

        public async Task<Dish>UpdateDishInMenuAsync(int menuId, int dishId, UpdateDishDTO dto)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            if (menu == null)
            {
                return null;
            }
            var dish = menu.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish == null)
            {
                return null;
            }
            
            // Fix: Parameters were reversed - DTO comes first, then dish
            var updatedDish = UpdateDishDTOMapper.UpdateDishByDTO(dto, dish);
            
            // Entity Framework tracks changes automatically, just need to update and save
            await _menuRepository.UpdateAsync(menu);
            await _menuRepository.SaveChangesAsync();
            return updatedDish;
        }

        public async Task<IActionResult> DeleteMenuByIdAsync(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return new NotFoundResult();
            }
            await _menuRepository.DeleteAsync(menu);
            await _menuRepository.SaveChangesAsync();
            return new NoContentResult();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            var menu = await _menuRepository.GetAllAsync();
            return menu;
        }
    }
}
