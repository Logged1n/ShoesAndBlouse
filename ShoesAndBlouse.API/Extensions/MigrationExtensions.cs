using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using PostgresDbContext dbContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
        
        dbContext.Database.Migrate();
    }
}