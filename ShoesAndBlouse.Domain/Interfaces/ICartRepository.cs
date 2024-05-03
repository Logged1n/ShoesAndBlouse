using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface ICartRepository
{
    Task AddItemToCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default);
    Task RemoveItemFromCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default);
    Task EditItemInCartAsync(int userId, OrderDetail item, CancellationToken cancellationToken = default);
    Task<Cart> GetCartAsync(int userId, CancellationToken cancellationToken = default);
    Task MakeOrderAsync(Cart userCart, CancellationToken cancellationToken = default);
}