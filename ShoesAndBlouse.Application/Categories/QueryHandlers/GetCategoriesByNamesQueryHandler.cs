using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoriesByNamesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesByNamesQuery, ICollection<Category>>
{
    public async Task<ICollection<Category>> Handle(GetCategoriesByNamesQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoriesByNames(request.categoryNames, cancellationToken);
    }
}