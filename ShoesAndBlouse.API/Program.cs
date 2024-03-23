using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Entities.Product;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.Infrastructure.Data;
using ShoesAndBlouse.Infrastructure.Repositories;
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

//Repository setup
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Caching Repository setup
builder.Services.AddScoped<IProductRepository, CachingProductRepository>();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Comment out only for docker usage
app.UseHttpsRedirection();

app.MapPost("/api/v1/products", async (IMediator mediator, Product product) =>
    {
        var createProduct = new CreateProduct { 
            Name = product.Name, 
            Description = product.Description,
            Price = product.Price
        };
    
        var createdProduct = await mediator.Send(createProduct);
    
        return Results.CreatedAtRoute("GetById", new { 
            createdProduct.Name,
            createdProduct.Description,
            createdProduct.Price
        }, createdProduct);
    })
    .WithName("CreateProduct");

app.MapGet("/api/v1/products/{id:int}", async (IMediator mediator, int id) =>
{
    var getProduct = new GetProductById { Id = id };
    var product = await mediator.Send(getProduct);
    return Results.Ok(product);
})
.WithName("GetProductById");

app.MapGet("/api/v1/products", async (IMediator mediator) =>
{
    var getCommand = new GetAllProducts();
    var products = await mediator.Send(getCommand);
    return Results.Ok(products);
})
.WithName("GetAllProducts");

app.Run();
