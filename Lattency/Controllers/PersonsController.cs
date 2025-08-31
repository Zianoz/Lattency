using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services;
using Lattency.Services.IServices;
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

        // PUT: api/Persons/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, [FromBody] PersonDTO dto)
        {
            var updatedPerson = await _personService.UpdatePersonAsync(id, dto);
            if (updatedPerson == null) return NotFound();
            return Ok(updatedPerson);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var isDeleted = await _personService.DeletePersonAsync(id);
            if (!isDeleted) return NotFound();
            return NoContent(); // 204 No Content is a common response for a successful deletion
        }
    }
}