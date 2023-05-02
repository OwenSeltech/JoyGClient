using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
	public class DishesEnjoyedRepository : IDishesEnjoyedRepository
	{
		private readonly DataContext _context;
		public DishesEnjoyedRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<bool> AddDishAsync(DishesEnjoyed dishesEnjoyed)
		{
			_context.Entry(dishesEnjoyed).State = EntityState.Added;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> UpdateDishAsync(DishesEnjoyed dishesEnjoyed)
		{
			_context.Entry(dishesEnjoyed).State = EntityState.Modified;
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<DishesEnjoyed>> GetAllDishesAsync()
		{
			return await _context.DishesEnjoyed
				.Include(x => x.User)
				.Include(x => x.Dishes)
				.ToListAsync();
		}
		public async Task<DishesEnjoyed> GetDishesByIdAsync(string id)
		{
			return await _context.DishesEnjoyed
				.Where(x => x.Id.ToString() == id.Trim())
				.Include(x => x.User)
				.Include(x => x.Dishes)
				.FirstOrDefaultAsync();
		}
	}
}
