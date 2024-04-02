using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Entities.Product;
using ShoesAndBlouse.Domain.Entities.User;

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
    public DbSet<Review> Review { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Category> Category { get; set; }
    //Add DbSets here
}