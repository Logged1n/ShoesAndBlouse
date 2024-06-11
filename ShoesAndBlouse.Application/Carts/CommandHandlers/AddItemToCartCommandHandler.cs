using MediatR;
using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.CommandHandlers;

public class AddItemToCartCommandHandler(ICartRepository cartRepository) : IRequestHandler<AddItemToCartCommand, Cart>
{
    public async Task<Cart> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemToCartAsync(request.userId, request.Item, cancellationToken);

        return await cartRepository.GetCartAsync(request.userId, cancellationToken);
    }
}