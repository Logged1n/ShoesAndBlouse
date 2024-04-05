using Microsoft.AspNetCore.Identity;
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
            //Add Repositories Scopes
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //Mediator Pattern Setup
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //Add DbContext
            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));
            //Setup Session Management
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //Setup ClaimsIdentity Authenticatio
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<PostgresDbContext>()
            .AddDefaultTokenProviders();;
            
            return services;
        }
    }
}