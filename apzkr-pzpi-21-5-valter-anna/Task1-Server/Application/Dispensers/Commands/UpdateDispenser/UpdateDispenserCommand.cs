using MediatR;

namespace Application.Dispensers.Commands.UpdateDispenser;

public record UpdateDispenserLocationCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public string Location { get; set; }
}
