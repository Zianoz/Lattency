using Lattency.Data;
using Lattency.DTOs;
using Lattency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeTablesController : ControllerBase
    {
        private readonly LattencyDBContext _context;

        public CafeTablesController(LattencyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CafeTable>>> GetCafeTables()
        {
            return await _context.CafeTables.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CafeTable>> CreateCafeTable([FromBody] CafeTableDTO dto)
        {
            var cafeTable = new CafeTable
            {
                Id = dto.Id,
                Capacity = dto.Capacity,
                Available = dto.Available,
                BildURL = dto.BildURL,
                Bookings = new List<PersonBookings>()
            };

            _context.CafeTables.Add(cafeTable);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCafeTables), new { id = cafeTable.Id }, cafeTable);
        }
    }
}
