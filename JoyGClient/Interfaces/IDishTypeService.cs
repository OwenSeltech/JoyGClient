using JoyGClient.DTOs;
using JoyGClient.Entities;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
    public interface IDishTypeService
    {
        Task<ResponseDto> AddDishType(DishTypeModel dishTypeModel);
        Task<ResponseDto> EditDishType(DishTypeModel dishTypeModel);
        Task<IEnumerable<DishTypes>> GetDishTypes();
        Task<DishTypes> GetDishTypeById(string typeId);
    }
}
