using Lattency.Models;

namespace Lattency.Services.IServices
{
    public interface IMenuService
    {
        Task<Menu> CreateMenuAsync(Menu menu);
        Task<Menu> GetMenuByIdAsync(int id);
    }
}
