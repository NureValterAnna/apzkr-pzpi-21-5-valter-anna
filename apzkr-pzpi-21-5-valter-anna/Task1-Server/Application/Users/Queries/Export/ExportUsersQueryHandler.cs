using Application.Interfaces;
using MediatR;
using Newtonsoft.Json;


namespace Application.Users.Queries.Export;

public class ExportUsersQueryHandler : IRequestHandler<ExportUsersQuery, string>
{
    private readonly IUserRepository _userRepository;
    public ExportUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(ExportUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return JsonConvert.SerializeObject(users);
    }
}
