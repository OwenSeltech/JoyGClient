using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class RestaurantClassificationRepository : IRestaurantClassificationRepository
    {
        private readonly DataContext _context;
        public RestaurantClassificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddClassificationAsync(RestaurantClassifications restaurantClassification)
        {
            _context.Entry(restaurantClassification).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClassificationAsync(RestaurantClassifications restaurantClassification)
        {
            _context.Entry(restaurantClassification).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<RestaurantClassifications>> GetAllClassificationsAsync()
        {
            return await _context.RestaurantClassifications.ToListAsync();
        }
        public async Task<RestaurantClassifications> GetClassificationByIdAsync(string id)
        {
            return await _context.RestaurantClassifications
                .Where(x => x.Id.ToString() == id.Trim())
                .Include(x => x.UpdatedBy)
                .Include(x => x.CreatedBy)
                .FirstOrDefaultAsync();
        }
        public async Task<RestaurantClassifications> GetClassificationByNameAsync(string name)
        {
            return await _context.RestaurantClassifications
                .Where(o => o.ClassificationName.ToLower().Trim() == name.ToLower().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public bool ClassificationExists(string classification)
        {
            return _context.RestaurantClassifications.Any(o => o.ClassificationName.ToLower().Trim() == classification.ToLower().Trim());
        }

    }
}
