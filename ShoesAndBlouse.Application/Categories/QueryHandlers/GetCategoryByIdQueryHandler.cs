using MediatR;
using ShoesAndBlouse.Application.Categories.Queries;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;

namespace ShoesAndBlouse.Application.Categories.QueryHandlers;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryByIdAsync(request.Id, cancellationToken);
        var categoryDto = CategoryMapper.MapToDto(category);

        return categoryDto;
    }
}