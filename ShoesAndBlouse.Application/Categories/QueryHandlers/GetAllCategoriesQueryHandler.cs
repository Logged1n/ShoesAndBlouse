using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);
        var categoryDtos = CategoryMapper.MapListToDto(categories);

        return categoryDtos;
    }
}