using MediatR;
using ShoesAndBlouse.Application.DTOs;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }
    // Dodaj inne właściwości
}