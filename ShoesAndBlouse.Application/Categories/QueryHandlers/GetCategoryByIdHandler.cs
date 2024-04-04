using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryById, Category?>
{
    public async Task<Category?> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryById(request.categoryId, cancellationToken);
    }
}