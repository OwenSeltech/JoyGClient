using JoyGClient.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Data
{
    public class DataContext : IdentityDbContext<AppUser, Roles, int, IdentityUserClaim<int>, UserRoles, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
            .IsRequired();

            builder.Entity<Roles>()
              .HasMany(ur => ur.UserRoles)
              .WithOne(u => u.Role)
              .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        }
        public DbSet<RestaurantClassifications> RestaurantClassifications { get; set; }
        public DbSet<DishTypes>  DishTypes { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dishes> Dishes  { get; set; }
        public DbSet<Preferences> Preferences { get; set; }

        add preferences table

    }
}
