using MediatR;

namespace Application.Dispensers.Commands.UpdateDispenserTempereture;

public record UpdateDispenserTemperetureCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public double StorageTemperature { get; set; }
}
