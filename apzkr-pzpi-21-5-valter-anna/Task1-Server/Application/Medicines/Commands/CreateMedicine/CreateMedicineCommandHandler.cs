using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IMapper _mapper;

    public CreateMedicineCommandHandler(IMedicineRepository medicineRepository, IMapper mapper)
    {
        _medicineRepository = medicineRepository;
        _mapper = mapper; 
    }

    public async Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        var newMedicineId = await _medicineRepository.CreateMedicine(request, cancellationToken);
        return newMedicineId;
    }
}
