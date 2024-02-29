using backend.Extensions.Data;
using backend.Interfaces;
using backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace backend.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }
    }
}