using JoyGClient.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JoyGClient.Models
{
    public class RestaurantModel
    {
        [Required(ErrorMessage = "The Restaurant Name field is required.")]
        public string RestaurantName { get; set; }
        public int SelectedClassificationsId { get; set; }

        public IEnumerable<SelectListItem>? Classifications { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Id { get; set; }
    }
}
