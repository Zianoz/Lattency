using Lattency.Data;
using Lattency.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Lattency.Models;
using Lattency.DTOs;

namespace Lattency.Repositories
{
    public class CafeTableRepository : ICafeTableRepository
    {
        private readonly LattencyDBContext _context;
        public CafeTableRepository(LattencyDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync()
        {
            return await _context.CafeTables.ToListAsync();
        }

        public async Task<IEnumerable<CafeTable>> GetAllAvailableCafeTablesAsync()
        {
            return await _context.CafeTables
                     .Where(t => t.Available == true)
                     .ToListAsync();
        }

        public async Task<CafeTable> GetCafeTableByIdAsync(int id)
        {
            return await _context.CafeTables.FindAsync(id);
        }

        public async Task<CafeTable>CreateCafeTableAsync(CafeTable cafeTable)
        {
            _context.CafeTables.AddAsync(cafeTable);
            await _context.SaveChangesAsync();
            return cafeTable;
        }

        public async Task DeleteCafeTableAsync(CafeTable cafeTable)
        {
            _context.CafeTables.Remove(cafeTable);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTableAsync(CafeTable cafeTable)
        {
            _context.CafeTables.Update(cafeTable);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
