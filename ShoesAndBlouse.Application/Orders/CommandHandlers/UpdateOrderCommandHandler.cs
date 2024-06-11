using MediatR;
using ShoesAndBlouse.Application.DTOs;
using ShoesAndBlouse.Application.Mappers;
using ShoesAndBlouse.Application.Orders.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.CommandHandlers;

public class UpdateOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository, IProductRepository productRepository) : IRequestHandler<UpdateOrderCommand, OrderDto?>
{

    public async Task<OrderDto?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
        if (orderToUpdate is null)
            return null;

        if (request.OrderStatus is not null)
            orderToUpdate.Status = request.OrderStatus.Value;

        if (request.BillingAddress is not null)
        {
            var billingAddress = await addressRepository.GetAddressByValuesAsync(request.BillingAddress.Line1,
                request.BillingAddress.Line2,
                request.BillingAddress.City,
                request.BillingAddress.Country,
                request.BillingAddress.PostalCode,
                cancellationToken);
            if (billingAddress is null)
            {
                billingAddress = new Address
                {
                    Line1 = request.BillingAddress.Line1,
                    Line2 = request.BillingAddress.Line2,
                    City = request.BillingAddress.City,
                    PostalCode = request.BillingAddress.PostalCode,
                    Country = request.BillingAddress.Country
                };
                orderToUpdate.BillingAddress = await addressRepository.CreateAddressAsync(billingAddress, cancellationToken) ?? throw new InvalidOperationException();
            }
        }
        
        if (request.ShippingAddress is not null)
        {
            var shippingAddress = await addressRepository.GetAddressByValuesAsync(request.ShippingAddress.Line1,
                request.ShippingAddress.Line2,
                request.ShippingAddress.City,
                request.ShippingAddress.Country,
                request.ShippingAddress.PostalCode,
                cancellationToken);
            if (shippingAddress is null)
            {
                shippingAddress = new Address
                {
                    Line1 = request.ShippingAddress.Line1,
                    Line2 = request.ShippingAddress.Line2,
                    City = request.ShippingAddress.City,
                    PostalCode = request.ShippingAddress.PostalCode,
                    Country = request.ShippingAddress.Country
                };
                orderToUpdate.ShippingAddress = await addressRepository.CreateAddressAsync(shippingAddress, cancellationToken);
            }
        }

        if (request.OrderDetails is not null)
        {
            orderToUpdate.Total.Amount = 0;
            foreach (var cartItem in request.OrderDetails)
            {
                var product = await productRepository.GetProductByIdAsync(cartItem.ProductId, cancellationToken);
                var detail = new OrderDetail
                {
                    Order = orderToUpdate,
                    Product = product ?? throw new ArgumentNullException(),
                    Qty = cartItem.Qty
                };
                orderToUpdate.OrderDetails.Add(detail);
                orderToUpdate.Total.Amount += product.Price.Amount * cartItem.Qty;
            }
        }
        await orderRepository.UpdateOrderAsync(orderToUpdate, cancellationToken);
        return OrderMapper.MapToDto(orderToUpdate);
    }
}