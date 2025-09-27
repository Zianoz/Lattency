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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu { Id = 4, Name = "Drinks" },
                new Menu { Id = 5, Name = "Pastries" }
            );

            // Seed Dishes
            modelBuilder.Entity<Dish>().HasData(
                // Drinks
                new Dish { Id = 1, DishName = "Espresso", Description = "Rich and bold single shot of espresso", Price = 25, IsPopular = true, MenuId = 4 },
                new Dish { Id = 2, DishName = "Cappuccino", Description = "Espresso with steamed milk and foam", Price = 39, MenuId = 4 },
                new Dish { Id = 3, DishName = "Latte", Description = "Smooth espresso with lots of steamed milk", Price = 42, IsPopular = true, MenuId = 4 },
                new Dish { Id = 4, DishName = "Iced Latte", Description = "Cold espresso with milk over ice", Price = 45, MenuId = 4 },
                new Dish { Id = 5, DishName = "Matcha Latte", Description = "Creamy green tea latte", Price = 49, MenuId = 4 },
                new Dish { Id = 6, DishName = "Hot Chocolate", Description = "Classic cocoa with whipped cream", Price = 35, MenuId = 4 },
                new Dish { Id = 7, DishName = "Chai Latte", Description = "Spiced tea latte with cinnamon and cardamom", Price = 44, MenuId = 4 },
                new Dish { Id = 15, DishName = "Brown Sugar Boba Tea", Description = "Sweet and creamy milk tea with brown sugar pearls", Price = 55, IsPopular = true, MenuId = 4 },
                new Dish { Id = 16, DishName = "Strawberry Boba Tea", Description = "Refreshing strawberry milk tea with chewy tapioca pearls", Price = 55, MenuId = 4 },

                // Pastries
                new Dish { Id = 8, DishName = "Croissant", Description = "Flaky butter croissant", Price = 29, MenuId = 5 },
                new Dish { Id = 9, DishName = "Pain au Chocolat", Description = "Buttery pastry filled with chocolate", Price = 34, MenuId = 5 },
                new Dish { Id = 10, DishName = "Cinnamon Bun", Description = "Sweet bun with cinnamon filling", Price = 32, IsPopular = true, MenuId = 5 },
                new Dish { Id = 11, DishName = "Blueberry Muffin", Description = "Soft muffin with fresh blueberries", Price = 36, MenuId = 5 },
                new Dish { Id = 12, DishName = "Carrot Cake", Description = "Moist cake with cream cheese frosting", Price = 45, MenuId = 5 },
                new Dish { Id = 13, DishName = "Cheesecake", Description = "Creamy cheesecake with berry topping", Price = 55, MenuId = 5 },
                new Dish { Id = 14, DishName = "Strawberry Cheesecake", Description = "Classic cheesecake topped with fresh strawberries", Price = 59, IsPopular = true, MenuId = 5 }
            );
        }

    }
}

