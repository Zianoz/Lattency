using Lattency.Models;
using Lattency.Repositories.IRepositories;
using Lattency.Services.IServices;

namespace Lattency.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<Menu> CreateMenuAsync (Menu menu)
        {
            await _menuRepository.AddAsync(menu);
            await _menuRepository.SaveChangesAsync();
            return menu;
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            return menu;
        }
    }
}
