using MediatR;
using ShoesAndBlouse.Application.Carts.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Carts.CommandHandlers;

public class AddItemToCartCommandHandler(ICartRepository cartRepository) : IRequestHandler<AddItemToCartCommand, Cart>
{
    public async Task<Cart> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        await cartRepository.AddItemToCartAsync(request.userId, request.Item, cancellationToken);

        return await cartRepository.GetCartAsync(request.userId, cancellationToken);
    }
}