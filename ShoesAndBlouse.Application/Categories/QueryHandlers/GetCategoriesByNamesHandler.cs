using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoriesByNamesHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesByNames, ICollection<Category>>
{
    public async Task<ICollection<Category>> Handle(GetCategoriesByNames request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoriesByNames(request.categoryNames, cancellationToken);
    }
}