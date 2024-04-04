using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoryByNameHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByName, Category?>
{
    public async Task<Category?> Handle(GetCategoryByName request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryByName(request.Name, cancellationToken);
    }
}