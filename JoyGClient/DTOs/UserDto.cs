
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace JoyGClient.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
        public IList<string> Roles { get; set; }
        public IEnumerable<Claim> claims  { get; set; }

    }
}
