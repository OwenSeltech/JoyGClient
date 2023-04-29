using System.ComponentModel.DataAnnotations;

namespace JoyGClient.Models
{
    public class DishTypeModel
    {
        [Required(ErrorMessage = "The Dish Type Name field is required.")]
        public string DishTypeName { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Id { get; set; }
    }
}
