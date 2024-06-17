using MediatR;

namespace Application.Users.Commands.ChangeRole;

public record ChangeRoleCommand : IRequest<int>
{
    public int Id { get; set; }
    public string NewRole { get; set; }
}
