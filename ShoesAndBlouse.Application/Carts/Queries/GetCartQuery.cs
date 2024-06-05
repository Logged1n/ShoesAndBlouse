using MediatR;
using ShoesAndBlouse.Application.DTOs;

namespace ShoesAndBlouse.Application.Carts.Queries;

public class GetCartQuery : IRequest<CartDto>
{
    public int userId { get; set; }
}