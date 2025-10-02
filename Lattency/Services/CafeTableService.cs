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
        public async Task<IEnumerable<CafeTableDTO>> GetAllAvailableCafeTablesAsync()
        {

            var tables = await _cafeTableRepository.GetAllAvailableCafeTablesAsync();

            var tableDTO = tables.Select(dto => new CafeTableDTO
            {
                Id = dto.Id,
                Available = dto.Available,
                Capacity = dto.Capacity,
                BildURL = dto.BildURL
            });
            return tableDTO;

        }

        public async Task<CafeTable> SetAvailabilityAsync(int id)
        {
            var cafeTable = await _cafeTableRepository.GetCafeTableByIdAsync(id);
            if (cafeTable != null)
            {
                cafeTable.Available = false;
                await _cafeTableRepository.UpdateTableAsync(cafeTable);
                await _cafeTableRepository.SaveChangesAsync();
                return cafeTable;
            }
            return null!;
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
                BildURL = dto.BildURL,
                Bookings = new List<Booking>()
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
