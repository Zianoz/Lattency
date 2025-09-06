using Lattency.Data;
using Lattency.Models;
using Lattency.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly LattencyDBContext _context;

        public MenuRepository(LattencyDBContext context)
        {
            _context = context;
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Menus.Include(m=>m.Dishes).FirstOrDefaultAsync(m => m.Id == id);
            //return await _context.Menus.FindAsync(id);
        }
        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _context.Menus.ToListAsync();
        }
        public async Task AddAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await Task.CompletedTask;
        }
        public async Task UpdateAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await Task.CompletedTask;
        }

    }
}
