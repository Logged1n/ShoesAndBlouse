using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByNameQuery, Category?>
{
    public async Task<Category?> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryByName(request.Name, cancellationToken);
    }
}