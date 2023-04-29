using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class DishTypeRepository : IDishTypeRepository
    {
        private readonly DataContext _context;
        public DishTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDishTypeAsync(DishTypes dishTypes)
        {
            _context.Entry(dishTypes).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDishTypeAsync(DishTypes dishTypes)
        {
            _context.Entry(dishTypes).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DishTypes>> GetAllDishTypesAsync()
        {
            return await _context.DishTypes.ToListAsync();
        }
        public async Task<DishTypes> GetDishTypeByIdAsync(string id)
        {
            return await _context.DishTypes
                .Where(x => x.Id.ToString() == id.Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<DishTypes> GetDishTypeByNameAsync(string name)
        {
            return await _context.DishTypes
                .Where(o => o.DishTypeName.ToLower().Trim() == name.ToLower().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public bool DishTypeExists(string dishType)
        {
            return _context.DishTypes.Any(o => o.DishTypeName.ToLower().Trim() == dishType.ToLower().Trim());
        }
    }
}
