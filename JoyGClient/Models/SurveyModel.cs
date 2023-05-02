using JoyGClient.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JoyGClient.Models
{
	public class SurveyModel
	{
		public string? Id { get; set; }
		public string? User { get; set; }
        [Required(ErrorMessage = "Please select restaurant.")]
        public int SelectedRestaurantId { get; set; }
		public IEnumerable<SelectListItem>? Restaurants { get; set; }
		[Required(ErrorMessage = "The Date Visited field is required.")]
		public DateTime DateVisited { get; set; }
		public string? AmbienceRating { get; set; }
		public string? ServiceRating { get; set; }
		public string? OverallRating { get; set; }
		[Required(ErrorMessage = "The Comments field is required.")]
		public string Comments { get; set; }
        [Required(ErrorMessage = "The Classification field is required.")]
        public string Classification { get; set; }
    }
}
