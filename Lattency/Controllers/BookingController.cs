using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lattency.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICafeTableService _cafeTableService;

        public BookingController(IBookingService bookingService, ICafeTableService cafeTableService)
        {
            _bookingService = bookingService;
            _cafeTableService = cafeTableService;
        }
        public async Task<IActionResult> Index()
        {
            var tables = await _cafeTableService.GetAllAvailableCafeTablesAsync();
            return View(tables);
        }



        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Booking>> CreateBooking([FromForm] BookingDTO dto)
        {
            int personId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
                return BadRequest("Table is not available at the requested time.");

            return Ok(booking);
        }
    }
}
