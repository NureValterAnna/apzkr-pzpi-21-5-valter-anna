using MediatR;

namespace Application.Dispensers.Commands.CreateDispenser;

public record CreateDispenserCommand : IRequest<int>
{
    public string DispensorName { get; set; }

    public string Location { get; set; }
}
