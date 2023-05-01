using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<bool> AddRestaurantAsync(Restaurant restaurant);
        Task<bool> UpdateRestaurantAsync(Restaurant restaurant);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<Restaurant> GetRestaurantsByIdAsync(string id);
        bool RestaurantExists(string restaurant);
    }
}
