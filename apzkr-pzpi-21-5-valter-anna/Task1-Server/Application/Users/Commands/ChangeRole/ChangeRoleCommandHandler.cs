using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.ChangeRole;

public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeRoleCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ChangeRole(request, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return request.Id;
    }
}
