using Microsoft.EntityFrameworkCore;

namespace Lattency.DTOs
{
    public class UpdateDishDTO
    {
        public string DishName { get; set; }
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public bool IsPopular { get; set; }
        public string? ImageURL { get; set; }
    }
}
