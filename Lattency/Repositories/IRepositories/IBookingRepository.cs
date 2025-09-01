using Lattency.Models;

namespace Lattency.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        //Get a single booking by ID
        Task<Booking?> GetByIdAsync(int id);

        //Get all bookings for a specific person
        Task<IEnumerable<Booking>> GetByPersonIdAsync(int personId);

        // Get all bookings for a specific table (used for checking availability)
        Task<IEnumerable<Booking>> GetByTableIdAsync(int tableId);

        // Get all bookings (for admin purposes)
        Task<IEnumerable<Booking>> GetAllAsync();

        // Add a new booking to the database
        Task AddAsync(Booking booking);

        // Delete a booking from the database
        Task DeleteAsync(Booking booking);

        // Persist all changes to the database
        Task SaveChangesAsync();
    }
}
