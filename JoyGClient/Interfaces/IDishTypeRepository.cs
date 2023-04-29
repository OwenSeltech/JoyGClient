using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IDishTypeRepository
    {
        Task<bool> AddDishTypeAsync(DishTypes dishTypes);
        Task<bool> UpdateDishTypeAsync(DishTypes dishTypes);
        Task<IEnumerable<DishTypes>> GetAllDishTypesAsync();
        Task<DishTypes> GetDishTypeByIdAsync(string id);
        Task<DishTypes> GetDishTypeByNameAsync(string name);
        public bool DishTypeExists(string dishType);
        
    }
}
