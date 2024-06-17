using MediatR;

namespace Application.Users.Commands.Login;

public record class LoginCommand : IRequest<string>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
