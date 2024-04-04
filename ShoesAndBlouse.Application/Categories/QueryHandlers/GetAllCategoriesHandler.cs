using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategories, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAll(cancellationToken);
    }
}