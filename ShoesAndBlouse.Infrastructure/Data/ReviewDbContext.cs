using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

public class ReviewDbContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public ReviewDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("Default"));
    }
    
    public DbSet<Review> Review { get; set; }
};