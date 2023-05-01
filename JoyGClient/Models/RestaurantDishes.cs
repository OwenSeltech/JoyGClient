using JoyGClient.Entities;

namespace JoyGClient.Models
{
    public class RestaurantDishes
    {
        public Restaurant Restaurant { get; set; }
        public IEnumerable<Dishes> Dishes { get; set; }
    }
}
