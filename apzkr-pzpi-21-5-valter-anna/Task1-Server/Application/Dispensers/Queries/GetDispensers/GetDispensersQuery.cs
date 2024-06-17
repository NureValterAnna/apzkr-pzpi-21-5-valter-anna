using MediatR;

namespace Application.Dispensers.Queries.GetDispensers;

public record GetDispensersQuery : IRequest<List<DispenserResponse>>
{
    public string? TemperatureUnit { get; set; } = String.Empty;
}
