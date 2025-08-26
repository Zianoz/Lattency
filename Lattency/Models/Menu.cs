using Microsoft.EntityFrameworkCore;

namespace Lattency.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public bool IsPopular { get; set; }
    }
}
