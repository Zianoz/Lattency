using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;

namespace Lattency.Services //Business logic layer
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository; //Looks for and finds PersonRepository

        //Looks for and finds IPersonRepository
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _personRepository.GetAllPersonsAsync();
        }

        //Gets DTO from PersonController > IPersonService
        public async Task<Person> CreatePersonAsync(PersonDTO dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password); //Hashes password using BCrypt
            var person = new Person //Creates a new Person Object
            {
                Name = dto.Name,
                Email = dto.Email,
                Number = dto.Number,
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = "Customer",
                Bookings = new List<PersonBookings>()
            };
            await _personRepository.AddPersonAsync(person);
            return person; //Passes Person Object to PersonRepository
        }

        public async Task<Person> UpdatePersonAsync(int id, PersonDTO dto)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null) return null;

            person.Name = dto.Name ?? person.Name;
            person.Email = dto.Email ?? person.Email;
            person.Number = dto.Number ?? person.Number;
            person.Username = dto.Username ?? person.Username;

            await _personRepository.UpdatePersonAsync(person);
            return person;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null) return false;

            await _personRepository.DeletePersonAsync(person);
            return true;
        }
    }
}
