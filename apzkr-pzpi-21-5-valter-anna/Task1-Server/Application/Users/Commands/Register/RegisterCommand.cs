using MediatR;

namespace Application.Users.Commands.Register;

public sealed record RegisterCommand : IRequest<string>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
