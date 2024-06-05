using MediatR;
using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Carts.CommandHandlers;

public class RemoveItemFromCartCommandHandler(ICartRepository cartRepository) : IRequestHandler<RemoveItemFromCartCommand, CartDto>
{
    public async Task<CartDto> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.RemoveItemFromCartAsync(request.userId, request.item , cancellationToken);
        return CartMapper.MapToDto(await cartRepository.GetCartAsync(request.userId, cancellationToken));
    }
}