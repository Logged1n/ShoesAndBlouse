using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Application;
using ShoesAndBlouse.Application.Abstractions;
using ShoesAndBlouse.Application.Product.Commands;
using ShoesAndBlouse.Application.Product.Queries;
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
//Product Repository setup
builder.Services.AddScoped<IProductRepository, ProductRepository>();
//Postgres Database setup
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseNpgsql(cs));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products/{id}", async (IMediator mediator, int id) =>
{
    var result = await mediator.Send(new GetProductById { Id = id });
    
    if (result != null) return Results.Ok(result);
    
    return Results.NotFound();
});

app.Run();
