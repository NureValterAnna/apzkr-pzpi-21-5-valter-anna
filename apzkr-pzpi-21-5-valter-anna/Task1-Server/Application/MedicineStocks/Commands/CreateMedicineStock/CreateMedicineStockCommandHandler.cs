using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MedicineStocks.Commands.CreateMedicineStock;

public class CreateMedicineStockCommandHandler : IRequestHandler<CreateMedicineStockCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IMedicineStockRepository _medicineStockRepository;

    public CreateMedicineStockCommandHandler(IMapper mapper, IMedicineStockRepository medicineStockRepository)
    {
        _mapper = mapper;
        _medicineStockRepository = medicineStockRepository;
    }

    public async Task<int> Handle(CreateMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var newMedicinestockId = await _medicineStockRepository.CreateMedicineStock(request, cancellationToken);
        return newMedicinestockId;
    }
}
