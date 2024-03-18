using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Infrastructure.Data;

    public class CategoryDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
    
        public CategoryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        }
    
        public DbSet<Category> Category { get; set; }
    };
