using Lattency.Data;
using Lattency.DTOs;
using Lattency.Models;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Repositories //Data access layer = DATABASE LOGIC ONLY!
{
    public class PersonRepository : IPersonRepository
    {
        private readonly LattencyDBContext _context;

        public PersonRepository(LattencyDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            var person = await _context.Persons.Include(p => p.Bookings).ToListAsync();
            return person;
        }
        public async Task<Person> GetPersonByEmailAsync(string email)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            return person;
        }

        //Adds Person Object to database.
        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Person person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
