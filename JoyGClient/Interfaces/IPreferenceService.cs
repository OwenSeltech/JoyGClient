using JoyGClient.DTOs;
using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IPreferenceService
    {
        Task<ResponseDto> AddPreference(Preferences preference);

    }
}
