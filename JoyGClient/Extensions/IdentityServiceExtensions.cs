using JoyGClient.Data;
using JoyGClient.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace JoyGClient.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<Roles>()
                .AddRoleManager<RoleManager<Roles>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<Roles>>()
                .AddEntityFrameworkStores<DataContext>()
               /* .AddDefaultTokenProviders()*/;

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                          .AddCookie("Cookies", options =>
                                          {
                                              //options.Cookie.Name = "MySessionCookie";
                                              options.LoginPath = "/Auth/Index";
                                              options.LogoutPath = "/Auth/Logout";
                                              options.Cookie.HttpOnly = true;
                                              options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                                              options.SlidingExpiration = true;
                                              options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                                          });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //    };
            //});

            return services;
        }

    }
}
