using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, int>
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IMapper _mapper;

    public DeleteMedicineCommandHandler(IMedicineRepository medicineRepository, IMapper mapper)
    {
        _medicineRepository = medicineRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var removedMedicineId = await _medicineRepository.DeleteMedicine(request, cancellationToken);
        return removedMedicineId;
    }
}
