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
