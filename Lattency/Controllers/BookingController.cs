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
        public async Task<IActionResult> Index(DateTime reservationStart, int numGuests)
        {
            var tables = await _cafeTableService.GetAllAvailableCafeTablesAsync(reservationStart, numGuests);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckAvailableTables([FromQuery] DateTime reservationStart, [FromQuery] int numGuests)
        {
            var tables = await _cafeTableService.GetAllAvailableCafeTablesAsync(reservationStart, numGuests);
            ViewBag.ReservationStart = reservationStart;
            ViewBag.NumGuests = numGuests;

            // Explicitly reuse the Index view
            return View("Index", tables);
        }

        //Form sends in datetime and numguests, method fetches all bookings from repository and checks if table is available
        //by using reservationstart +- 2h window checkign against reservationend and reservationstart of existing bookings

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Booking>> CreateBooking([FromForm] BookingDTO dto)
        {
            int personId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (personId == 0)
                return Unauthorized("User not found.");

            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
                return BadRequest("Table is not available at the requested time.");

            return Ok(booking);
        }
    }
}
