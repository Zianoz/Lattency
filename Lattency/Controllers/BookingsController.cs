using Lattency.Data;
using Lattency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly LattencyDBContext _context;

        public BookingsController(LattencyDBContext context)
        {
            _context = context;
        }



        //[HttpPost]
        //public async Task<ActionResult> CreateBooking([FromBody] PersonBookings booking)
        //{
        //    var table = await _context.CafeTables
        //}

    }
}


//Be able to book if you are logged in as customer, checks if table is available, if so, create booking and set table to unavailable. The table will be booked for 2 hours before and after its booked time. Also assign the booking to its corresponding customer. If the table is not available, return a message saying so.