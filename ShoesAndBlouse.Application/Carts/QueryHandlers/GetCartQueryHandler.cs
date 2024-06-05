using MediatR;
using ShoesAndBlouse.Application.Carts.Queries;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Carts.QueryHandlers;

public class GetCartQueryHandler(ICartRepository cartRepository) : IRequestHandler<GetCartQuery, CartDto>
{

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(request.userId, cancellationToken);
        return CartMapper.MapToDto(cart);
    }
}