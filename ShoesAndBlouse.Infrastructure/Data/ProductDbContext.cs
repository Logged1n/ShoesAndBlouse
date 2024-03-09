using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace ShoesAndBlouse.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public ProductDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("Default"));
    }
    
    public DbSet<Product> Product { get; set; }
};