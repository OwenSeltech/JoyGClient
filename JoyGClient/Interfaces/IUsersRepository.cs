using JoyGClient.Entities;

namespace JoyGClient.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(string id);
        Task<AppUser> GetUserByUsernameAsync(string username);
    }
}
