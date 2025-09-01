using Microsoft.EntityFrameworkCore;

namespace Lattency.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public bool IsPopular { get; set; }
        public string ImageURL { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }
    }
}
