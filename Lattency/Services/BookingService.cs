using Lattency.DTOs;
using Lattency.Models;
using Lattency.Repositories;
using Lattency.Repositories.IRepositories;
using Lattency.Services.IServices;

namespace Lattency.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking?> CreateBookingAsync(int personId, int tableId, DateTime reservationStart, int numGuests)
        {
            //Booking window: 2h before and 2h after
            var startWindow = reservationStart.AddHours(-2);
            var endWindow = reservationStart.AddHours(2);

            //Check overlapping bookings for this table
            var tableBookings = await _bookingRepository.GetByTableIdAsync(tableId);
            bool overlap = tableBookings.Any(b =>
                (startWindow < b.ReservationEnd) && (endWindow > b.ReservationStart)
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
                CreatedAt = DateTime.UtcNow
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> DeleteBookingAsync(int bookingId, int personId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null || booking.FK_PersonId != personId)
                return false;

            await _bookingRepository.DeleteAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return true;
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
                PersonName = b.Person.Name
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
                PersonName = b.Person.Name
            });
        }
    }
}
