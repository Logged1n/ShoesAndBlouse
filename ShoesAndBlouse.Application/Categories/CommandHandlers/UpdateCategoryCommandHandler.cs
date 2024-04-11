using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetCategoryById(request.CategoryId, cancellationToken);

        if (existingCategory is null)
            return false;

        if (request.Name is not null)
            existingCategory.Name = request.Name;
        if (request.Product is not null)
            existingCategory.Products = request.Product;

        await _categoryRepository.UpdateCategory(existingCategory, cancellationToken);

        return true;
    }
}