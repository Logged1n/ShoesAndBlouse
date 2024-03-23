using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoesAndBlouse.Domain.Entities.Product;

namespace ShoesAndBlouse.Infrastructure.Data;

public class PostgresDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public PostgresDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("Default"));
    }
    
    public DbSet<Product> Product { get; set; }
    //Add DbSets here
};