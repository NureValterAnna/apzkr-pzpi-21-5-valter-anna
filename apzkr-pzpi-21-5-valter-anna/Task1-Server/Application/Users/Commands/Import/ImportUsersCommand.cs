using MediatR;

namespace Application.Users.Commands.Import;

public record ImportUsersCommand : IRequest<Unit>
{
    public string Json { get; set; }
}

