using Lattency.DTOs;
using Lattency.Models;

namespace Lattency.Repositories.IRepositories
{
    public interface ICafeTableRepository
    {
        Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync();
        Task<CafeTable> GetCafeTableByIdAsync(int id);
        Task<IEnumerable<CafeTable>> GetAllAvailableCafeTablesAsync();
        Task <CafeTable> CreateCafeTableAsync(CafeTable cafeTable);
        Task DeleteCafeTableAsync(CafeTable cafeTable);
        Task UpdateTableAsync(CafeTable cafeTable);
        Task SaveChangesAsync();

    }
} 
