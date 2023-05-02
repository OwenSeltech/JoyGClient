using JoyGClient.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Interfaces
{
    public interface IPreferenceRepository
    {

       Task<bool> AddPreferenceAsync(Preferences preferences);
       Task<bool> UpdatePreferenceAsync(Preferences preferences);

       Task<IEnumerable<Preferences>> GetAllPreferencesAsync();
        
       Task<Preferences> GetPreferencesByIdAsync(string id);
        
    }
}
