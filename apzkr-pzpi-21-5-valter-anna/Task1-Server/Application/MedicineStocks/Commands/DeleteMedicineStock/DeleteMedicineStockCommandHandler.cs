using Application.Interfaces;
using Application.Medicines.Commands.DeleteMedicine;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MedicineStocks.Commands.DeleteMedicineStock;

public class DeleteMedicineStockCommandHandler : IRequestHandler<DeleteMedicineStockCommand, int>
{
    private readonly IMedicineStockRepository _medicineStockRepository;
    private readonly IMapper _mapper;

    public DeleteMedicineStockCommandHandler(IMedicineStockRepository medicineStockRepository, IMapper mapper)
    {
        _medicineStockRepository = medicineStockRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(DeleteMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var removedMedicineStockId = await _medicineStockRepository.DeleteMedicineStock(request, cancellationToken);
        return removedMedicineStockId;
    }
}
