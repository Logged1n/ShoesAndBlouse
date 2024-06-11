using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;
using ShoesAndBlouse.Infrastructure.Extensions;
using ShoesAndBlouse.Infrastructure.Repositories;

namespace ShoesAndBlouse.Infrastructure
{
    public static class DependencyInjection
    {
        public  static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Repositories Scopes
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CachedCartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            
            //Mediator Pattern Setup
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            //Add DbContext
            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            //Allow DateTime mapping in Postgres
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //Setup Session and Caching Management
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "RedisInstance";
            });
            
            //Setup Identity
            services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<PostgresDbContext>();
            
            //Extension Methods initializing database
            services.ApplyMigrations();
            
            return services;
        }
    }
}