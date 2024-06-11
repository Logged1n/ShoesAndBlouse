using MediatR;
using ShoesAndBlouse.Application.Orders.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Enums;
using ShoesAndBlouse.Domain.Interfaces;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.Application.Orders.CommandHandlers;

public class CreateOrderCommandHandler(IUserRepository userRepository,
    ICartRepository cartRepository,
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IAddressRepository addressRepository) : IRequestHandler<CreateOrderCommand>
{

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //Get required entities
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
        if(user is null)
            return;
        var cart = await cartRepository.GetCartAsync(request.UserId, cancellationToken);
        if(cart is null)
            return;
        Address? billingAddress = null, shippingAddress = null;
        if (request.BillingAddress is not null)
        {
            billingAddress = await addressRepository.GetAddressByValuesAsync(request.BillingAddress.Line1,
                request.BillingAddress.Line2,
                request.BillingAddress.City,
                request.BillingAddress.Country,
                request.BillingAddress.PostalCode,
                cancellationToken);
            if (billingAddress is null)
            {
                Address address = new Address
                {
                    Line1 = request.BillingAddress.Line1,
                    Line2 = request.BillingAddress.Line2,
                    City = request.BillingAddress.City,
                    Country = request.BillingAddress.Country,
                    PostalCode = request.BillingAddress.PostalCode
                };
                billingAddress = await addressRepository.CreateAddressAsync(address, cancellationToken);
            }
        }
        if (request.ShippingAddress is not null)
        {
            shippingAddress = await addressRepository.GetAddressByValuesAsync(request.ShippingAddress.Line1,
                request.ShippingAddress.Line2,
                request.ShippingAddress.City,
                request.ShippingAddress.Country,
                request.ShippingAddress.PostalCode,
                cancellationToken);
            if (shippingAddress is null)
            {
                Address address = new Address
                {
                    Line1 = request.ShippingAddress.Line1,
                    Line2 = request.ShippingAddress.Line2,
                    City = request.ShippingAddress.City,
                    Country = request.ShippingAddress.Country,
                    PostalCode = request.ShippingAddress.PostalCode
                };
                shippingAddress = await addressRepository.CreateAddressAsync(address, cancellationToken);
            }
        }
        //map cart items to orderDetails
        Order createdOrder = new Order
        {
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            Status = OrderStatus.Open,
            User = user ?? throw new ArgumentNullException(),
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress ?? throw new ArgumentNullException(),
            OrderDetails = [],
            Total = new Money("PLN", 0)
        };

        foreach (var item in cart.CartItems)
        {
            var orderDetail = new OrderDetail
            {
                Order = createdOrder,
                Product = await productRepository.GetProductByIdAsync(item.ProductId, cancellationToken) ?? throw new ArgumentNullException(),
                Qty = item.Qty
            };
            createdOrder.OrderDetails.Add(orderDetail);
            createdOrder.Total.Amount += orderDetail.Product.Price.Amount;
        }
        //save order in database
        await orderRepository.CreateOrderAsync(createdOrder, cancellationToken);
    }
}