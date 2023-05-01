using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
    public interface IRestaurantService
    {
        Task<ResponseDto> AddRestaurant(RestaurantModel restaurantModel);
        Task<ResponseDto> EditRestaurant(RestaurantModel restaurantModel);
        Task<IEnumerable<Restaurant>> GetRestaurants();
        Task<Restaurant> GetRestaurantById(string restId);
    }
}
