using MediatR;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.Queries;

public class GetCartQuery : IRequest<Cart>
{
    public int userId { get; set; }
}