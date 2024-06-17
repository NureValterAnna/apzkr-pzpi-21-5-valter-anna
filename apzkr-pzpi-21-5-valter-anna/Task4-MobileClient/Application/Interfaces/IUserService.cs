using Application.Users.Commands;

namespace Application.Interfaces;

public interface IUserService
{
    Task Login(LoginCommand request);
}