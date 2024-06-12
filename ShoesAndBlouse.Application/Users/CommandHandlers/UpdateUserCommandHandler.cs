using MediatR;
using ShoesAndBlouse.Application.Users.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
namespace ShoesAndBlouse.Application.Users.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return false;
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Surname = request.Surname;
        // Update other properties

        var updatedUser = await _userRepository.UpdateUserAsync(user, cancellationToken);
        return updatedUser != null;
    }
}