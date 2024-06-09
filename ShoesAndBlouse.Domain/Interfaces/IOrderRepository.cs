using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Enums;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IOrderRepository
{
    Task<ICollection<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ICollection<Order>> GetAllUserOrderAsync(User user, CancellationToken cancellationToken = default);
    Task<ICollection<Order>> GetAllNotCompletedAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default);
    Task CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(Order toUpdate, CancellationToken cancellationToken = default);
    Task UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default);
}