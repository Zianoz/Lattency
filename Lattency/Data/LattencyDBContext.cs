using Microsoft.EntityFrameworkCore;
using Lattency.Models;

namespace Lattency.Data
{
    public class LattencyDBContext : DbContext
    {
        public LattencyDBContext(DbContextOptions<LattencyDBContext> options)
            : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CafeTable> CafeTables { get; set; }
    }
}

