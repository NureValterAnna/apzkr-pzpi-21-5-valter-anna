using Application.Users.Queries.GetUser;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery : IRequest<UserResponse>
{
    public string Email { get; set; }
}
