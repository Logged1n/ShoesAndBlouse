using MediatR;

namespace ShoesAndBlouse.Application.Categories.Commands;

public sealed record DeleteCategoryCommand(int categoryId) : IRequest<bool>
{
    
}