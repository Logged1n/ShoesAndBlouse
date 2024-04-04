using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await _categoryRepository.GetCategoryById(request.categoryId, cancellationToken);

        if (categoryToDelete is null)
            return false;

        await _categoryRepository.DeleteCategory(categoryToDelete.Id, cancellationToken);

        return true;
    }
}