using JoyGClient.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Interfaces
{
	public interface IDishesEnjoyedRepository
	{
		Task<bool> AddDishAsync(DishesEnjoyed dishesEnjoyed);
		Task<bool> UpdateDishAsync(DishesEnjoyed dishesEnjoyed);
		Task<IEnumerable<DishesEnjoyed>> GetAllDishesAsync();
		Task<DishesEnjoyed> GetDishesByIdAsync(string id);
		
	}
}
