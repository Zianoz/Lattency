using Lattency.DTOs;
using Lattency.Models;

namespace Lattency.Services.IServices
{
    public interface ICafeTableService
    {
        Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync();
        Task<CafeTable> GetCafeTableByIdAsync(int id);
        Task<CafeTable> CreateCafeTableAsync(CafeTableDTO cafeTable);
        Task<bool> DeleteCafeTableAsync(int id);
    }
}
