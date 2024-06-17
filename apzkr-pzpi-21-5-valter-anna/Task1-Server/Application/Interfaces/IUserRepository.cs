using Application.Users.Commands.ChangeRole;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using Application.Users.Queries.GetUserByEmail;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<string> Register(RegisterCommand request, CancellationToken cancellationToken);

    public Task<string> Login(LoginCommand request, CancellationToken cancellationToken);

    Task<int> DeleteUser(DeleteUserCommand request, CancellationToken cancellationToken);

    public Task ChangeRole(ChangeRoleCommand request, CancellationToken cancellationToken);

    public Task<User> GetByEmailAsync(GetUserByEmailQuery request, CancellationToken cancellationToken);
}
