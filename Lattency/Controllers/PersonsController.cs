using Lattency.Data;
using Lattency.DTOs;
using Lattency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Lattency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {

        private readonly LattencyDBContext _context;

        public PersonsController(LattencyDBContext context)
        {
            _context = context;
        }

        // Read all available Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _context.Persons.Include(p => p.Bookings).ToListAsync();
        }

        // Creates a new person using PersonDTO
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] PersonDTO dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var person = new Person
            {
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number,
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = "Customer", // Default role assignment
                Bookings = new List<PersonBookings>()
            };


            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersons), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, [FromBody] PersonDTO dto)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return NotFound();

            person.Name = dto.Name ?? person.Name;
            person.Email = dto.Email ?? person.Email;
            person.Number = dto.Number ?? person.Number;
            person.Username = dto.Username ?? person.Username;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error updating user: {ex.Message}");
            }

            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }
    }
}
