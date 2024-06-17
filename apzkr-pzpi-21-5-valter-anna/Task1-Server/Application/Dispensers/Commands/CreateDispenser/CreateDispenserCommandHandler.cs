using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Dispensers.Commands.CreateDispenser;

public class CreateDispenserCommandHandler : IRequestHandler<CreateDispenserCommand, int>
{
    private readonly IDispenserRepository _dispenserRipository;
    private readonly IMapper _mapper;

    public CreateDispenserCommandHandler(IDispenserRepository dispenserRipository, IMapper mapper)
    {
        _dispenserRipository = dispenserRipository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDispenserCommand request, CancellationToken cancellationToken)
    {
        var newDispenserId = await _dispenserRipository.CreateDispenser(request, cancellationToken);
        return newDispenserId;
    }
}
