using MediatR;
using ShoesAndBlouse.Application.Users.Commands;
using ShoesAndBlouse.Domain.Interfaces;
namespace ShoesAndBlouse.Application.Users.CommandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.DeleteUserAsync(request.Id, cancellationToken);
    }
}