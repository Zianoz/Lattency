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
        
        public IActionResult Index()
        {
            return View(Enumerable.Empty<CafeTableDTO>());
        }

        //Form sends in datetime and numguests, method fetches all bookings from repository and checks if table is available
        //by using reservationstart +- 2h window checking against reservationend and reservationstart of existing bookings
        [HttpGet]
        public async Task<IActionResult> CheckAvailableTables(CafeTableSearchModel input)
        {
            DateTime reservationStart = input.ReservationStart;
            int numGuests = input.NumGuests;

            var tables = await _cafeTableService.GetAllAvailableCafeTablesAsync(reservationStart, numGuests);

            // Pass the search criteria to ViewBag so they can be used in hidden fields
            ViewBag.ReservationStart = reservationStart.ToString("yyyy-MM-ddTHH:mm");
            ViewBag.NumGuests = numGuests;

            // Explicitly reuse the Index view
            return View("Index", tables);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] BookingDTO dto)
        {
            var personIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(personIdClaim) || !int.TryParse(personIdClaim, out int personId) || personId == 0)
            {
                TempData["ErrorMessage"] = "Please sign in to create a booking";
                return RedirectToAction("Index", "Login");
            }

            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
            {
                TempData["ErrorMessage"] = "Table is not available at the requested time.";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = $"Booking confirmed! Table {dto.CafeTableId} for {dto.NumGuests} guests on {dto.ReservationStart:g}";
            return RedirectToAction("Index");
        }
    }
}
