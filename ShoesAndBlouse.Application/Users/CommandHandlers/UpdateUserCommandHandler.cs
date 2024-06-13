using MediatR;
using ShoesAndBlouse.Application.Users.Commands;
using ShoesAndBlouse.Domain.Entities;
using ShoesAndBlouse.Domain.Interfaces;
namespace ShoesAndBlouse.Application.Users.CommandHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IAddressRepository addressRepository)
    {
        _userRepository = userRepository;
        _addressRepository = addressRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return false;
        }
        if(request.Name is not null) 
            user.Name = request.Name;
        if(request.Email is not null)
            user.Email = request.Email;
        if(request.Surname is not null)
            user.Surname = request.Surname;
        if (request.Address is not null)
        {
            var address = new Address
            {
                Line1 = request.Address.Line1,
                Line2 = request.Address.Line2,
                Country = request.Address.Country,
                City = request.Address.City,
                PostalCode = request.Address.PostalCode
            };
            user.Address = address;
        }
            

        var updatedUser = await _userRepository.UpdateUserAsync(user, cancellationToken);
        return updatedUser != null;
    }
}