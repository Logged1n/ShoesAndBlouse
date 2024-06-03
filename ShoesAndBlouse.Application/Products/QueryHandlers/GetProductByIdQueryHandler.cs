using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Products.QueryHandlers;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            var productDto = ProductMapper.MapToDto(product);
            return productDto;
        }
        catch(Exception exception)
        {
            return new ProductDto
            {
                Id = 0,
                Name = "This Product does not Exists!",
                Price = new Money("NOT", 0m),
                Description = exception.Message,
                Categories = [],
                PhotoUrl = "Images/Product/0.png"
            };
        }

        
    }
}