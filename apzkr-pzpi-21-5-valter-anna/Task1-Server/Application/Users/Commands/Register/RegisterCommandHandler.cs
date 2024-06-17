using Application.Interfaces;
using MediatR;

namespace Application.Users.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = await _userRepository.Register(request, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return jwtToken;
    }
}
