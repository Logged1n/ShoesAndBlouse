using Microsoft.EntityFrameworkCore;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class OrderDetailRepository(PostgresDbContext context) : IOrderDetailRepository
{

    public async Task<OrderDetail?> GetOrderDetailByIdAsync(int orderDetailId, CancellationToken cancellationToken = default)
    {
        var orderDetail = await context.OrderDetails
            .Include(od => od.Product)
            .FirstOrDefaultAsync(od => od.Id == orderDetailId, cancellationToken);
            
        return orderDetail;
    }
    public async Task<List<OrderDetail>> GetOrderDetailsAsync(Order order, CancellationToken cancellationToken = default)
    {
        var orderDetails = await context.OrderDetails
            .Where(od => od.Order.Id == order.Id)
            .Include(od => od.Product)
            .ToListAsync(cancellationToken);
        
        return orderDetails;
    }
    public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default)
    {
         context.OrderDetails.Add(orderDetail);
         await context.SaveChangesAsync(cancellationToken);
         return orderDetail;
    }
    public async Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail toUpdate, CancellationToken cancellationToken = default)
    {
        context.OrderDetails.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken);
        return toUpdate;
    }
}