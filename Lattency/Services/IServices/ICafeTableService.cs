using Lattency.DTOs;
using Lattency.Models;

namespace Lattency.Services.IServices
{
    public interface ICafeTableService
    {
        Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync();
        Task<IEnumerable<CafeTableDTO>> GetAllAvailableCafeTablesAsync(DateTime reservationStart, int numGuests);
        Task<CafeTable> GetCafeTableByIdAsync(int id);
        Task<CafeTable> CreateCafeTableAsync(CafeTableDTO cafeTable);
        Task<bool> DeleteCafeTableAsync(int id);
        Task<(IEnumerable<CafeTable> tables, IEnumerable<Booking> bookings)> UpdateBookingAndTableStatus();
    }
}
