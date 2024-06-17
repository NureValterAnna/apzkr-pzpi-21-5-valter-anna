using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = await _userRepository.Login(request, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return jwtToken;
    }
}
