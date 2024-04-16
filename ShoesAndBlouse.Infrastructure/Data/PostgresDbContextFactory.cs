using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShoesAndBlouse.Infrastructure.Data;

public class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
{
    //This class helps with doing EF Core migrations when
    //"An error occurred while accessing the Microsoft.Extensions.Hosting services.
    //Continuing without the application service provider. Error: Some services are not able to be constructed
    //(Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[ShoesAndBlouse.Application.Products.Commands.CreateProduct,ShoesAndBlouse.Domain.Entities.Product] Lifetime:
    //Transient ImplementationType: ShoesAndBlouse.Application.Products.CommandHandlers.CreateProductHandler':
   // Unable to resolve service for type 'ShoesAndBlouse.Domain.Interfaces.ICategoryRepository' while attempting to activate 'ShoesAndBlouse.Application.Products.CommandHandlers.CreateProductHandler'.)"

    public PostgresDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5432;Database=ShoesAndBlouse;User Id=postgres;Password=admin");

        return new PostgresDbContext(optionsBuilder.Options);
    }
}