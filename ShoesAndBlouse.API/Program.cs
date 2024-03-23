using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.Infrastructure.Data;
using ShoesAndBlouse.Infrastructure.Repositories.Cache;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Clean Architecture Setup
builder.Services
    .AddApplication()
    .AddInfrastructure();

//Decorator pattern for Caching repositories
builder.Services.Decorate<IProductRepository, CachingProductRepository>();

//Redis Caching setup
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});

//Postgres Database setup
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PostgresDbContext>(opt => opt.UseNpgsql(cs));

//Mediator pattern setup
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));

//Enable Controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Comment out only for docker usage
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
