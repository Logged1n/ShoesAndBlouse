using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;
using ShoesAndBlouse.Infrastructure.Repositories;

namespace ShoesAndBlouse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            
            return services;
        }
    }
}