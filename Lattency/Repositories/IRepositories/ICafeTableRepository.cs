using Lattency.Models;

namespace Lattency.Repositories.IRepositories
{
    public interface ICafeTableRepository
    {
        Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync();
        Task<CafeTable> GetCafeTableByIdAsync(int id);
        Task <CafeTable> CreateCafeTableAsync(CafeTable cafeTable);

    }
} 
