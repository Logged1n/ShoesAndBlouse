using ShoesAndBlouse.Domain.Entities;

namespace ShoesAndBlouse.Domain.Interfaces;

public interface IOrderDetailRepository
{
    Task<OrderDetail?> GetOrderDetailByIdAsync(int orderDetailId, CancellationToken cancellationToken = default);
    Task<List<OrderDetail>> GetOrderDetailsAsync(Order order, CancellationToken cancellationToken = default);
    Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default);
    Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail toUpdate, CancellationToken cancellationToken = default);
}