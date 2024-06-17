using MediatR;

namespace Application.Dispensers.Commands.DeleteDispenser;

public record DeleteDispenserCommand : IRequest<int>
{
    public int Id { get; set; }
}
