using MediatR;
using ShoesAndBlouse.Application.DTOs;
namespace ShoesAndBlouse.Application.Users.Queries;

public partial class GetAllUsersQuery : IRequest<List<UserDto>> { }

