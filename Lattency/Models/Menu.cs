using Microsoft.EntityFrameworkCore;

namespace Lattency.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //one menu has many dishes
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
