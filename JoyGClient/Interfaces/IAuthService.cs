using JoyGClient.DTOs;
using JoyGClient.Models;

namespace JoyGClient.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginModel loginDto);
        Task<UserDto> Register(RegisterModel registerDto);
    }
}
