using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.MedicineStocks.Commands.UpdateMedicineStock;

public class UpdateMedicineStockCommandHandler : IRequestHandler<UpdateMedicineStockCommand, int>
{
    private readonly IMedicineStockRepository _medicineStockRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMedicineStockCommandHandler(IMedicineStockRepository medicineStockRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _medicineStockRepository = medicineStockRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var stockId = await _medicineStockRepository.UpdateMedicineStock(request, cancellationToken);
        return stockId;
    }
}
