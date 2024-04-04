using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ShoesAndBlouse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(assemblies));

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
};