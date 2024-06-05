using MediatR;
using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Application.Carts.Queries;

public class GetCartQuery : IRequest<Cart>
{
    public int userId { get; set; }
}