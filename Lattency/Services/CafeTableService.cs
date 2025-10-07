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
        private readonly IBookingRepository _bookingRepository;

        public CafeTableService(ICafeTableRepository cafeTableRepository, IBookingRepository bookingRepository)
        {
            _cafeTableRepository = cafeTableRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<CafeTable>> GetAllCafeTablesAsync()
        {
            return await _cafeTableRepository.GetAllCafeTablesAsync(); 
        }

        public async Task<IEnumerable<CafeTableDTO>> GetAllAvailableCafeTablesAsync(DateTime reservationStart, int numGuests) //took me too long to write this asjidasjidjias
        {
            //Fetch current state of bookings and cafetables then update statuses
            var (tables, bookings) = await UpdateBookingAndTableStatus();

            //Filter available tables for the requested reservation
            var availableTables = tables.Where(table =>
                table.Capacity >= numGuests &&
                table.Available &&
                !bookings.Any(booking =>
                    booking.FK_CafeTableId == table.Id &&
                    booking.Status == "Active" &&
                    reservationStart < booking.ReservationEnd &&
                    reservationStart.AddHours(2) > booking.ReservationStart
                )
            );

            var tableDTO = availableTables.Select(dto => new CafeTableDTO
            {
                Id = dto.Id,
                Available = dto.Available,
                Capacity = dto.Capacity,
                BildURL = dto.BildURL
            });

            return tableDTO;
        }

        public async Task<(IEnumerable<CafeTable> tables, IEnumerable<Booking> bookings)> UpdateBookingAndTableStatus()
        {
            var tables = await _cafeTableRepository.GetAllCafeTablesAsync();
            var bookings = await _bookingRepository.GetAllAsync();
            
            var now = DateTime.Now;
            foreach (var booking in bookings)
            {
                if (now > booking.ReservationEnd && booking.Status != "Expired" && booking.CafeTable.Available == false)
                {
                    booking.Status = "Expired";
                    booking.CafeTable.Available = true;
                    await _bookingRepository.UpdateAsync(booking);
                    await _cafeTableRepository.UpdateTableAsync(booking.CafeTable);
                }
                else if (now >= booking.ReservationStart && now <= booking.ReservationEnd && booking.Status != "Ongoing")
                {
                    booking.Status = "Ongoing";
                    await _bookingRepository.UpdateAsync(booking);
                }
                else if (now < booking.ReservationStart && booking.Status != "Booked")
                {
                    booking.Status = "Booked";
                    await _bookingRepository.UpdateAsync(booking);
                }
            }

            await _cafeTableRepository.SaveChangesAsync();
            await _bookingRepository.SaveChangesAsync();

            return (tables, bookings);
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
