using JoyGClient.Data;
using JoyGClient.Data.Repositories;
using JoyGClient.Helpers;
using JoyGClient.Interfaces;
using JoyGClient.Services;
using Microsoft.EntityFrameworkCore;

namespace JoyGClient.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IClassificationService, ClassificationService>();
			services.AddScoped<IDishTypeService, DishTypeService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IDishesService, DishesService>();
            services.AddScoped<IRestaurantClassificationRepository, RestaurantClassificationRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
			services.AddScoped<IDishTypeRepository, DishTypeRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();


            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContextFactory<DataContext>(options => options.UseSqlServer(
                connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();

                }), ServiceLifetime.Transient);
            return services;
        }

    }
}
