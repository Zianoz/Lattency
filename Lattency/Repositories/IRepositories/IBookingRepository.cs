using Lattency.Models;

namespace Lattency.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<PersonBookings>> GetAllBookingsAsync();
    }
}
