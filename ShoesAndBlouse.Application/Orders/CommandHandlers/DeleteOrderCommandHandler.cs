using MediatR;
using ShoesAndBlouse.Application.Orders.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.CommandHandlers;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand>
{

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await orderRepository.DeleteOrderAsync(request.orderId, cancellationToken);
    }
}