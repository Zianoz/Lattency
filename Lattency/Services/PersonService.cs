using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace Lattency.Services //Business logic layer
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository; //Looks for and finds PersonRepository
        private readonly IConfiguration _configuration; //Allows access to appsettings.json

        //Looks for and finds IPersonRepository
        public PersonService(IPersonRepository personRepository, IConfiguration configuration)
        {
            _personRepository = personRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _personRepository.GetAllPersonsAsync();
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var person = await _personRepository.GetPersonByUsernameAsync(dto.Username);
            if (person == null || !BCrypt.Net.BCrypt.Verify(dto.Password, person.PasswordHash))
            {
                return null;  //Invalid credentials
            }

            //Generate JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, person.Username),
                new Claim(ClaimTypes.Role, person.Role),
                new Claim(ClaimTypes.NameIdentifier, person.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                Bookings = new List<Booking>()
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
