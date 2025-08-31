using Lattency.Services.IServices;
using Lattency.DTOs;
using Lattency.Models;
using Lattency.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Lattency.Repositories;

namespace Lattency.Services
{
    public class CafeTableService : ICafeTableService
    {
        private readonly ICafeTableRepository _cafeTableRepository;

        public CafeTableService(ICafeTableRepository cafeTableRepository)
        {
            _cafeTableRepository = cafeTableRepository;
        }

        public async Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync()
        {
            return await _cafeTableRepository.GetAllCafeTablesAsync();
        }

        public async Task<CafeTable> GetCafeTableByIdAsync(int id)
        {
            return await _cafeTableRepository.GetCafeTableByIdAsync(id);
        }

        public async Task<CafeTable> CreateCafeTableAsync(CafeTableDTO dto)
        {
            var cafeTable = new CafeTable 
            {
                Capacity = dto.Capacity,
                Available = dto.Available,
                BildURL = dto.BildURL,
                Bookings = new List<PersonBookings>()
            };

            await _cafeTableRepository.CreateCafeTableAsync(cafeTable);
            return cafeTable;
        }

        public async Task<bool> DeleteCafeTableAsync(int id)
        {
            var cafeTable = await _cafeTableRepository.GetCafeTableByIdAsync(id);
            if (cafeTable == null) return false;

            await _cafeTableRepository.DeleteCafeTableAsync(cafeTable);
            return true;
        }
    }
}
