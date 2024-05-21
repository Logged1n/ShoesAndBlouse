using MediatR;
using ShoesAndBlouse.Application.Categories.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.CommandHandlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(request.categoryId, cancellationToken);

        if (categoryToDelete is null)
            return;

        await _categoryRepository.DeleteCategoryAsync(categoryToDelete.Id, cancellationToken);
        
    }
}