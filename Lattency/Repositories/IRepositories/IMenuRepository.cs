using Lattency.Models;

namespace Lattency.Repositories.IRepositories
{
    public interface IMenuRepository
    {
        Task<Menu> GetByIdAsync(int id);
        Task<IEnumerable<Menu>> GetAllAsync();
        Task AddAsync(Menu menu);
        Task SaveChangesAsync();
        Task DeleteAsync(Menu menu);
        Task UpdateAsync(Menu menu);
    }
}
