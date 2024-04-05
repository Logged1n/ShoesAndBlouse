using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class CreateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategory, Category>
{
    public async Task<Category> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Products = request.Products,
        };
        return await categoryRepository.CreateCategory(category, cancellationToken);
    }
}