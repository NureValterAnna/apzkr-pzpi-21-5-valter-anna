using Application.Interfaces;
using Application.Users.Commands.ChangeRole;
using Application.Users.Commands.Delete;
using Application.Users.Commands.Login;
using Application.Users.Commands.Register;
using Application.Users.Queries.GetUserByEmail;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly IMapper _mapper;

    public UserRepository(AppDbContext context, ITokenService tokenService, IPasswordService passwordService, IMapper mapper) : base(context)
    {
        _tokenService = tokenService;
        _passwordService = passwordService;
        _mapper = mapper;
    }

    public async Task<string> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is not null)
        {
            throw new UserAlreadyExistsException();
        }

        var salt = _passwordService.GenerateSalt();
        var hash = _passwordService.HashPassword(request.Password, salt);

        user = _mapper.Map<User>(request);
        user.PasswordSalt = Convert.ToBase64String(salt);
        user.PasswordHash = hash;
        user.Role = "patient";

        await base.Create(user);

        return _tokenService.CreateToken(user);
    }

    public async Task<string> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var hash = user.PasswordHash;
        var salt = user.PasswordSalt;

        var isValid = _passwordService.VerifyPassword(request.Password, hash, salt);
        if (isValid)
        {
            return _tokenService.CreateToken(user);
        }

        throw new PasswordNotMatchException();
    }

    public async Task<int> DeleteUser(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var removedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (removedUser is null)
        {
            throw new DispenserNotFoundException();
        }
        await base.Delete(removedUser);
        return removedUser.Id;
    }

    public override Task Create(User entity)
    {
        if (_context.Users.Any(x => x.Email == entity.Email))
        {
            throw new UserAlreadyExistsException();
        }
        return base.Create(entity);
    }

    public async Task ChangeRole(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.Role = request.NewRole;

        await base.Update(user, cancellationToken);
    }

    public override Task<User> GetAsync(int id, CancellationToken cancellationToken) 
    {
        var user = base.GetAsync(id, cancellationToken);
        if (user.Result is null)
        {
            throw new UserNotFoundException();
        }
        return user;
    }

    public override Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var user = base.GetAllAsync(cancellationToken);
        return user;
    }

    public async Task<User> GetByEmailAsync(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        return user;
    }
}
