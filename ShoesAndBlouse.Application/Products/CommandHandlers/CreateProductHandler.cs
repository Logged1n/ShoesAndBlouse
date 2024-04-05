using MediatR;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Products.CommandHandlers;

public class CreateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository) : IRequestHandler<CreateProduct, Product>
{
    public async Task<Product> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        //Get List of Categories form categoryRepository
        var categories = await categoryRepository.GetCategoriesByNames(request.CategoryNames, cancellationToken);
         //Create new product based on request
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Categories = categories,
            PhotoPath = request.PhotoPath,
        };
        //Save it in database
        return await productRepository.CreateProduct(product, cancellationToken);
    }
}
