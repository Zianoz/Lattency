using Lattency.DTOs;
using Lattency.Models;

namespace Lattency.Mappers.DTOMappers
{
    public static class UpdateDishDTOMapper
    {
        public static Dish UpdateDishByDTO(UpdateDishDTO dto, Dish dish)
        {
            dish.DishName = dto.DishName;
            dish.Description = dto.Description;
            dish.Price = dto.Price;
            dish.IsPopular = dto.IsPopular;
            dish.ImageURL = dto.ImageURL;
            
            return dish;
        }
    }
}
