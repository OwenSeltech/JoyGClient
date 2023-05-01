using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JoyGClient.Models
{
    public class DishModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "The Dish Name field is required.")]

        public string DishName { get; set; }
        [Required(ErrorMessage = "The Dish Description Name field is required.")]
        public string DishDescription { get; set; }
        public string? RestaurantId { get; set; }
        public int? SelectedDishTypeId { get; set; }
        public IEnumerable<SelectListItem>? DishTypes { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
