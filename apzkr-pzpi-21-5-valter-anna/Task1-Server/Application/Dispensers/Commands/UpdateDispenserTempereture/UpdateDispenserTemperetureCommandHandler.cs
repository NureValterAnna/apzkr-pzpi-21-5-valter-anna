using Application.Dispensers.Commands.UpdateDispenser;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Dispensers.Commands.UpdateDispenserTempereture;

public class UpdateDispenserTemperetureCommandHandler : IRequestHandler<UpdateDispenserTemperetureCommand, Unit>
{
    private readonly IDispenserRepository _dispenserRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDispenserTemperetureCommandHandler(IDispenserRepository dispenserRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _dispenserRepository = dispenserRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDispenserTemperetureCommand request, CancellationToken cancellationToken)
    {
        await _dispenserRepository.UpdateTemperature(request, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return Unit.Value;
    }
}
