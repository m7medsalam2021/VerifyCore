using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace AuthSystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
