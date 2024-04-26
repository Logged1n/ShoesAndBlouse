using MediatR;

namespace ShoesAndBlouse.Application.Categories.Commands;

public class UpdateCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public List<int>? ProductsIds { get; set; }
}