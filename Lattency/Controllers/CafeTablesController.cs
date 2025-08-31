using Lattency.Data;
using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeTablesController : ControllerBase
    {
        private readonly ICafeTableService _cafeTableService; 

        public CafeTablesController(ICafeTableService cafeTableService)
        {
            _cafeTableService = cafeTableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CafeTable>>> GetCafeTables()
        {
            var cafeTables = await _cafeTableService.GetAllCafeTablesAsync();
            return Ok(cafeTables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CafeTable>> GetCafeTableByIdAsync(int id)
        {
            var cafeTable = await _cafeTableService.GetCafeTableByIdAsync(id);
            if (cafeTable == null)
            {
                return NotFound();
            }
            return Ok(cafeTable);
        }

        [HttpPost]
        public async Task<ActionResult<CafeTable>> CreateCafeTable([FromBody] CafeTableDTO dto)
        {
            var cafeTable = await _cafeTableService.CreateCafeTableAsync(dto);
            return Ok(cafeTable);
        }
    }
}
