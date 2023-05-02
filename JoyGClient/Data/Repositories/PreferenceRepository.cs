using JoyGClient.Entities;
using JoyGClient.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data.Repositories
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly DataContext _context;
        public PreferenceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPreferenceAsync(Preferences preferences)
        {
            _context.Entry(preferences).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePreferenceAsync(Preferences preferences)
        {
            _context.Entry(preferences).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Preferences>> GetAllPreferencesAsync()
        {
            return await _context.Preferences
                .Include(x => x.Classifications)
                .ToListAsync();
        }
        public async Task<Preferences> GetPreferencesByIdAsync(string id)
        {
            return await _context.Preferences
                .Where(x => x.Id.ToString() == id.Trim())
                .Include(x => x.Classifications)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        
    }
}
