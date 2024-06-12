using MediatR;
namespace ShoesAndBlouse.Application.Users.Commands;
public class UpdateUserCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Surname { get; set; }
    // Dodaj inne właściwości
}