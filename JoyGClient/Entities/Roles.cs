using Microsoft.AspNetCore.Identity;

namespace JoyGClient.Entities
{
    public class Roles : IdentityRole<int>
    {
        public ICollection<UserRoles> UserRoles { get; set; }

    }
}
