using Lattency.DTOs;
using Lattency.Models;
using Lattency.Models.ViewModels;
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

        //Form sends in datetime and numguests, method fetches all bookings from repository and checks if table is available
        //by using reservationstart +- 2h window checkign against reservationend and reservationstart of existing bookings
        [HttpGet]
        public async Task<IActionResult> CheckAvailableTables(CafeTableSearchModel input)
        {
            DateTime reservationStart = input.ReservationStart;
            int numGuests = input.NumGuests;

            var tables = await _cafeTableService.GetAllAvailableCafeTablesAsync(reservationStart, numGuests);

            // Explicitly reuse the Index view
            return View("Index", tables);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Booking>> CreateBooking([FromForm] BookingDTO dto)
        {
            int personId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (personId == 0)
                return Unauthorized("Please sign in to create a booking");

            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
                return BadRequest("Table is not available at the requested time.");

            return Ok(booking);
        }
    }
}
