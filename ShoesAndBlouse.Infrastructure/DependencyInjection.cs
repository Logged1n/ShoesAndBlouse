using Microsoft.Extensions.DependencyInjection;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Infrastructure.Repositories;
//using ShoesAndBlouse.Infrastructure.Repositories.Cache;

namespace ShoesAndBlouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<IProductRepository, CachingProductRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));
        return services;
    }
}