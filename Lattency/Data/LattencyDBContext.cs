using Microsoft.EntityFrameworkCore;
using Lattency.Models;

namespace Lattency.Data
{
    public class LattencyDBContext : DbContext
    {
        public LattencyDBContext(DbContextOptions<LattencyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<PersonBookings> Bookings { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
