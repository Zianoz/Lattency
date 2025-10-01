using Azure;
using Lattency.DTOs;
using Lattency.Models;
using Lattency.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Lattency.Services //Business logic layer
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository; //Looks for and finds PersonRepository
        private readonly IConfiguration _configuration; //Allows access to appsettings.json
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Looks for and finds IPersonRepository
        public PersonService(IPersonRepository personRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _personRepository = personRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<FetchPersonDTO>> GetAllPersonsAsync()
        {
            var people = await _personRepository.GetAllPersonsAsync();

            return people.Select(p => new FetchPersonDTO
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                Number = p.Number,
                Username = p.Username,
            });
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var person = await _personRepository.GetPersonByEmailAsync(dto.Email);
            if (person == null || !BCrypt.Net.BCrypt.Verify(dto.Password, person.PasswordHash))
            {
                return null;  //Invalid credentials
            }

            //Generate JWT
            var claims = new[] //Creats identity claims which becomes User.Identity and User.IsInRole(...).
            {
                new Claim(ClaimTypes.Email, person.Email),
                new Claim(ClaimTypes.Role, person.Role),
                new Claim(ClaimTypes.NameIdentifier, person.Id.ToString())
            };

            //Generates a signed JWT using secret from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Store JWT in HttpOnly cookie
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", jwtToken, new CookieOptions
            {
                HttpOnly = true, 
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return jwtToken;

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
