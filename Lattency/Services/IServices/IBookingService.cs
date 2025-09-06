using Lattency.DTOs;
using Lattency.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lattency.Services.IServices
{
    public interface IBookingService
    {
        //Create a booking for a person
        Task<Booking?> CreateBookingAsync(int personId, int tableId, DateTime startTime, int numGuests);

        //Delete a booking for a person
        Task<ActionResult<Booking>> DeleteBookingAsync(int íd);

        //Get all bookings for a specific person
        Task<IEnumerable<BookingResponseDTO>> GetBookingsByPersonIdAsync(int personId);

        //Get all bookings (admin purpose)
        Task<IEnumerable<BookingResponseDTO>> GetAllPersonBookingsAsync();
    }
}
