using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Infrastructure;
using ShoesAndBlouse.Infrastructure.Data;
using ShoesAndBlouse.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Clean Architecture Setup
builder.Services
    .AddApplication()
    .AddInfrastructure();
//Products Repository setup
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//Postgres Database setup
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseNpgsql(cs));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));
//builder.WebHost.UseUrls("http://localhost:7076");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/api/products/{id}", async (IMediator mediator, int id) =>
{
    var getProduct = new GetProductById { Id = id };
    var product = await mediator.Send(getProduct);
    return Results.Ok(product);
})
.WithName("GetProductById");

app.MapPost("/api/products", async (IMediator mediator, Product product) =>
{
    var createProduct = new CreateProduct { Name = product.Name, 
        Description = product.Description };
    var createdProduct = await mediator.Send(createProduct);
    return Results.CreatedAtRoute("GetById", new { createdProduct.Name,
        createdProduct.Description },
        createdProduct);
});

app.MapGet("/api/products", async (IMediator mediator) =>
{
    var getCommand = new GetAllProducts();
    var products = await mediator.Send(getCommand);
    return Results.Ok(products);
});
app.Run();
