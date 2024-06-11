using MediatR;
using ShoesAndBlouse.Application.Carts.Queries;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Carts.QueryHandlers;

public class GetCartQueryHandler(ICartRepository cartRepository) : IRequestHandler<GetCartQuery, Cart>
{

    public async Task<Cart> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(request.userId, cancellationToken);
        return cart;
    }
}