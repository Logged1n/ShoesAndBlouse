using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Products.Queries;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.QueryHandlers;

public class GetAllProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll(cancellationToken);
        //Map products to productDtos
        var productDtos = ProductMapper.MapListToDto(products);
        
        return productDtos;
    }
}