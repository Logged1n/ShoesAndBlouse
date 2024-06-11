using Microsoft.EntityFrameworkCore;

using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Infrastructure.Data;

namespace ShoesAndBlouse.Infrastructure.Repositories;

public class OrderRepository(PostgresDbContext context) : IOrderRepository
{
    public async Task<ICollection<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders
            .Include(o => o.User)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderDetails)
            .ToListAsync(cancellationToken);
        return orders;
    }

    public async Task<ICollection<Order>> GetAllUserOrderAsync(User user, CancellationToken cancellationToken = default)
    {
        var userOrders = await context.Orders
            .Where(o => o.User.Id == user.Id)
            .Include(o => o.User)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderDetails)
            .ToListAsync(cancellationToken);
        return userOrders;
    }

    public async Task<ICollection<Order>> GetAllNotCompletedAsync(CancellationToken cancellationToken = default)
    {
        var orders = await context.Orders
            .Where(o => o.Status != OrderStatus.Completed)
            .Include(o => o.User)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderDetails)
            .ToListAsync(cancellationToken);
        return orders;
    }

    public Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var order = context.Orders
            .Include(o => o.User)
            .Include(o => o.ShippingAddress)
            .Include(o => o.BillingAddress)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        return order;
    }

    public async Task CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderAsync(Order toUpdate, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == toUpdate.Id, cancellationToken);
        if(order is null)
            return;
        context.Orders.Update(toUpdate);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        if(order is null)
            return;
        order.Status = status;
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        if(order is null)
            return;
        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
    }
}