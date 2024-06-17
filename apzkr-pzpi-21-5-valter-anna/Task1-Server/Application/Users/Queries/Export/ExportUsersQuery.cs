using MediatR;

namespace Application.Users.Queries.Export;

public record ExportUsersQuery : IRequest<string>
{
}
