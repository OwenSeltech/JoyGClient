using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IDishesRepository
    {
        Task<bool> AddDishAsync(Dishes dishes);
        Task<bool> UpdateDishAsync(Dishes dishes);
        Task<IEnumerable<Dishes>> GetAllDishesAsync();
        Task<Dishes> GetDishByIdAsync(string id);
        Task<IEnumerable<Dishes>> GetDishesByRestaurantAsync(Restaurant restaurant);
        bool DishExists(string dish);
    }
}
