using System.Security.Claims;
using Lattency.Data;
using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsAPIController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsAPIController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //Create a booking
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Booking>> CreateBooking(BookingDTO dto)
        {
            int personId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); //Gets the logged-in users ID from the JWT token
            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
                return BadRequest("Table is not available at the requested time.");

            return Ok("Booking created!");
        }

        //Get bookings for logged-in customer
        [HttpGet("mybookings")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IEnumerable<BookingResponseDTO>>> GetMyBookings()
        {
            var personIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(personIdStr))
                return Unauthorized("User ID not found in token.");

            int personId = int.Parse(personIdStr);
            var bookings = await _bookingService.GetBookingsByPersonIdAsync(personId);

            return Ok(bookings);
        }

        //Get all bookings (admin)
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BookingResponseDTO>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllPersonBookingsAsync();
            return Ok(bookings);
        }

        //Delete a booking
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _bookingService.DeleteBookingAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");
            return Ok("Booking was deleted successfully!");
        }

    }

}


//Be able to book if you are logged in as customer, checks if table is available, if so, create booking and set table to unavailable.
//The table will be booked for 2 hours before and after its booked time.
//Also assign the booking to its corresponding customer.
//If the table is not available, return a message saying so.