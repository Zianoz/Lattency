using Lattency.Data;
using Lattency.Models;
using Lattency.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lattency.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly LattencyDBContext _context;

        public BookingRepository(LattencyDBContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Person)
                .Include(b => b.CafeTable)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetByPersonIdAsync(int personId)
        {
            return await _context.Bookings
                .Include(b => b.Person)
                .Include(b => b.CafeTable)
                .Where(b => b.FK_PersonId == personId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByTableIdAsync(int tableId)
        {
            return await _context.Bookings
                .Include(b => b.Person)
                .Include(b => b.CafeTable)
                .Where(b => b.FK_CafeTableId == tableId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Person)
                .Include(b => b.CafeTable)
                .ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
