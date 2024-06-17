using MediatR;

namespace Application.Users.Commands.Delete;

public record DeleteUserCommand : IRequest<int>
{
    public int Id { get; set; }
}
