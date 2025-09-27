using Lattency.DTOs;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lattency.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            return View();
        }


        // POST: Handle form submission
        [HttpPost]
        public async Task<IActionResult> Index(BookingDTO dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "You must log in to finalize your booking.";
                return View();
            }

            int personId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var booking = await _bookingService.CreateBookingAsync(personId, dto.CafeTableId, dto.ReservationStart, dto.NumGuests);

            if (booking == null)
            {
                ViewBag.Message = "Table is not available at the requested time.";
            }
            else
            {
                ViewBag.Message = "Booking confirmed! 🎉";
            }

            return View();
        }
    }
}
