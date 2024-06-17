using Application.Interfaces;
using Application.MedicineStocks.Commands.UpdateMedicineStock;
using AutoMapper;
using MediatR;

namespace Application.Dispensers.Commands.UpdateDispenser;

public class UpdateDispenserLocationCommandHandler : IRequestHandler<UpdateDispenserLocationCommand, Unit>
{
    private readonly IDispenserRepository _dispenserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDispenserLocationCommandHandler(IDispenserRepository dispenserRepository, IUnitOfWork unitOfWork)
    {
        _dispenserRepository = dispenserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDispenserLocationCommand request, CancellationToken cancellationToken)
    {
        await _dispenserRepository.UpdateLocation(request, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return Unit.Value;
    }
}
