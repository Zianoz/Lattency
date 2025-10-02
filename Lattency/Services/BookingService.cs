using Lattency.DTOs;
using Lattency.Models;
using Lattency.Repositories;
using Lattency.Repositories.IRepositories;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        //Injecting CafeTableService to set CafeTable to false when booking is set.
        private readonly ICafeTableService _CafeTableService;

        public BookingService(IBookingRepository bookingRepository, ICafeTableService cafeTableService)
        {
            _bookingRepository = bookingRepository;
            _CafeTableService = cafeTableService;
        }

        public async Task<Booking?> CreateBookingAsync(int personId, int tableId, DateTime reservationStart, int numGuests)
        {
            //Booking window: 2h before and 2h after
            var startWindow = reservationStart.AddHours(-2);
            var endWindow = reservationStart.AddHours(2);

            //Check overlapping bookings for this table
            var tableBookings = await _bookingRepository.GetByTableIdAsync(tableId);

            //check against active bookings
            bool overlap = tableBookings
                .Where(b => b.ReservationEnd > DateTime.UtcNow) //ignore expired
                .Any(b =>
                    (startWindow < b.ReservationEnd) &&
                    (endWindow > b.ReservationStart)
                );

            if (overlap)
                return null; //Table is not available

            var booking = new Booking
            {
                FK_PersonId = personId,
                FK_CafeTableId = tableId,
                ReservationStart = reservationStart,
                ReservationEnd = endWindow,
                NumGuests = numGuests,
                Status = "Active",
                CreatedAt = DateTime.UtcNow
            };

            var table = await _CafeTableService.GetCafeTableByIdAsync(tableId);

            if (table == null)
                return null; // or throw an exception if table doesn't exist
            await _CafeTableService.SetAvailabilityAsync(tableId);
            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return booking;
        }

        public async Task<ActionResult<Booking>> DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return new NotFoundObjectResult("Booking was not found");

            await _bookingRepository.DeleteAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return new OkObjectResult(booking);
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetBookingsByPersonIdAsync(int personId)
        {
            var bookings = await _bookingRepository.GetByPersonIdAsync(personId);

            return bookings.Select(b => new BookingResponseDTO
            {
                Id = b.Id,
                CafeTableId = b.FK_CafeTableId,
                ReservationStart = b.ReservationStart,
                ReservationEnd = b.ReservationEnd,
                NumGuests = b.NumGuests,
                PersonId = b.FK_PersonId,
                PersonName = b.Person.FullName
            });
        }

        public async Task<IEnumerable<BookingResponseDTO>> GetAllPersonBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();

            return bookings.Select(b => new BookingResponseDTO
            {
                Id = b.Id,
                CafeTableId = b.FK_CafeTableId,
                ReservationStart = b.ReservationStart,
                ReservationEnd = b.ReservationEnd,
                NumGuests = b.NumGuests,
                PersonId = b.FK_PersonId,
                PersonName = b.Person.FullName,
                Status = b.ReservationEnd > DateTime.UtcNow ? "Expired" : "Active"
            });
        }
    }
}
