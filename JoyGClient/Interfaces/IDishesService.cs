using JoyGClient.Data.Repositories;
using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
    public interface IDishesService
    {
        Task<ResponseDto> AddDish(DishModel dishModel);
        Task<ResponseDto> EditDish(DishModel dishModel);
        Task<IEnumerable<Dishes>> GetDishess();
        Task<Dishes> GetDishById(string id);
        Task<IEnumerable<Dishes>> GetDishesByRestaurantAsync(Restaurant restaurant);
    }
}
