using Lattency.Data;
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
        public async Task<ActionResult<CafeTable>> CreateCafeTable([FromBody] CafeTable table)
        {
            _context.CafeTables.Add(table);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCafeTables), new { id = table.Id }, table);
        }
    }
}
