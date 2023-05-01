using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly DataContext _context;
        public DishesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDishAsync(Dishes dishes)
        {
            _context.Entry(dishes).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDishAsync(Dishes dishes)
        {
            _context.Entry(dishes).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Dishes>> GetAllDishesAsync()
        {
            return await _context.Dishes
                .Include(x => x.DishType)
                .Include(x => x.Restaurant)
                .ToListAsync();
        }
        public async Task<Dishes> GetDishByIdAsync(string id)
        {
            return await _context.Dishes
                .Where(x => x.Id.ToString() == id.Trim())
                .Include(x => x.DishType)
                .Include(x => x.Restaurant)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Dishes>> GetDishesByRestaurantAsync(Restaurant restaurant)
        {
            return await _context.Dishes
                .Where(x => x.Restaurant == restaurant)
                .Include(x => x.DishType)
                .Include(x => x.Restaurant)
                .AsNoTracking()
                .ToListAsync();
        }
        public bool DishExists(string dish)
        {
            return _context.Dishes.Any(o => o.DishName.ToLower().Trim() == dish.ToLower().Trim());
        }
    }
}
