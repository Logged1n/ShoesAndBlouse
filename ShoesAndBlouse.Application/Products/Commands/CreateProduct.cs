using MediatR;

namespace ShoesAndBlouse.Application.Products.Commands;

public class CreateProduct : IRequest<Domain.Entities.Product>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
