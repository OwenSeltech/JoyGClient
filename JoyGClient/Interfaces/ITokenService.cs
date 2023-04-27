using System.Security.Claims;

namespace JoyGClient.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(IEnumerable<Claim> claims);
    }
}
