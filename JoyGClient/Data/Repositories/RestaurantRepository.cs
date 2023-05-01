using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;
        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRestaurantAsync(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRestaurantAsync(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _context.Restaurants
                .Include(x => x.RestaurantClassification)
                .Include(x => x.Dishes)
                .ToListAsync();
        }
        public async Task<Restaurant> GetRestaurantsByIdAsync(string id)
        {
            return await _context.Restaurants
                .Where(x => x.Id.ToString() == id.Trim())
                .Include(x => x.RestaurantClassification)
                .Include(x => x.Dishes)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public bool RestaurantExists(string restaurant)
        {
            return _context.Restaurants.Any(o => o.RestaurantName.ToLower().Trim() == restaurant.ToLower().Trim());
        }
    }
}
