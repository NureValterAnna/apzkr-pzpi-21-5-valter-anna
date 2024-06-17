using MediatR;

namespace Application.Users.Queries.GetUser;

public record GetUserQuery : IRequest<UserResponse>
{
    public int Id { get; set; }
}
