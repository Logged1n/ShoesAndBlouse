using MediatR;
using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Carts.CommandHandlers;

public class AddItemToCartCommandHandler(ICartRepository cartRepository) : IRequestHandler<AddItemToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<CartDto> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemToCartAsync(request.userId, request.Item, cancellationToken);

        return CartMapper.MapToDto(await cartRepository.GetCartAsync(request.userId, cancellationToken));
    }
}