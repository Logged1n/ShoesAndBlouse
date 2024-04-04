using MediatR;

namespace ShoesAndBlouse.Application.Categories.Commands;

public sealed record DeleteCategory(int categoryId) : IRequest<bool>
{
    
}