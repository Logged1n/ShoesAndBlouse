using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICartRepository
{
    Task AddItemToCartAsync(int userId, CartItem item, CancellationToken cancellationToken = default);
    Task RemoveItemFromCartAsync(int userId, CartItem item, CancellationToken cancellationToken = default);
    Task<Cart?> GetCartAsync(int userId, CancellationToken cancellationToken = default);
    Task ClearCartAsync(int userId, CancellationToken cancellationToken = default);
}