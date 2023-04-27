using JoyGClient.Entities;
using Microsoft.AspNetCore.Identity;

namespace JoyGClient.Data
{
    public class Seed
    {
        public static async Task SeedRoles(RoleManager<Roles> roleManager)
        {

            var roles = new List<Roles>
            {
                new Roles{Name = "EndUser"},
                new Roles{Name ="Admin"},
                new Roles{Name ="DataAdmin"},

            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

    }
}
