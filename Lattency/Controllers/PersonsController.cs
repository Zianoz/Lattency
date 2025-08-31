using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lattency.Controllers //API Layer
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        //Provides service to container when controller is made ig
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Person>> LoginAsync([FromBody] LoginDTO dto)
        {

            var token = await _personService.LoginAsync(dto);
            if (token == null)
            {
                return Unauthorized("Invalid credentials or insufficient permissions.");
            }

            return Ok(new { Token = token });
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        // POST: api/Persons
        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] PersonDTO dto)
        {
            var person = await _personService.CreatePersonAsync(dto);
            return CreatedAtAction(nameof(GetPersons), new { id = person.Id }, person);
        }

        // PUT: api/Persons/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, [FromBody] PersonDTO dto)
        {
            var updatedPerson = await _personService.UpdatePersonAsync(id, dto);
            if (updatedPerson == null) return NotFound();
            return Ok(updatedPerson);
        }

        // DELETE: api/Persons/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var isDeleted = await _personService.DeletePersonAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent();
        }
    }
}