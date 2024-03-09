using ShoesAndBlouse.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

namespace ShoesAndBlouse.Application.Product.Queries;

public sealed record GetProductById : IRequest<Domain.Entities.Product>
{
    public int Id { get; set; }
    
}