using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;

namespace Application.Users.Commands.Import;

public class ImportUsersCommandHandler : IRequestHandler<ImportUsersCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImportUsersCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ImportUsersCommand request, CancellationToken cancellationToken)
    {
        var users = JsonConvert.DeserializeObject<List<User>>(request.Json);

        foreach (var user in users)
        {
            _userRepository.Create(user);
        }

        await _unitOfWork.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}
