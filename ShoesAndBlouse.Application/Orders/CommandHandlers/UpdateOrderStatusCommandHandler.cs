using MediatR;
using ShoesAndBlouse.Application.Orders.Commands;
using ShoesAndBlouse.Domain.Interfaces;

namespace ShoesAndBlouse.Application.Orders.CommandHandlers;

public class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderStatusCommand>
{

    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
         await orderRepository.UpdateOrderStatusAsync(Convert.ToInt32(request.OrderId), request.Status, cancellationToken);
    }
}