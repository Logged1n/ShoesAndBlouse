using MediatR;
using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.CommandHandlers;

public class RemoveItemFromCartCommandHandler(ICartRepository cartRepository) : IRequestHandler<RemoveItemFromCartCommand, Cart>
{
    public async Task<Cart> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.RemoveItemFromCartAsync(request.userId, request.item , cancellationToken);
        return await cartRepository.GetCartAsync(request.userId, cancellationToken);
    }
}