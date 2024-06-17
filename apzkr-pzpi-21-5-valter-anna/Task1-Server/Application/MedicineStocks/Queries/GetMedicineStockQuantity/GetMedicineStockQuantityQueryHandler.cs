
using Application.Interfaces;
using MediatR;

namespace Application.MedicineStocks.Queries.GetMedicineStockQuantity;

public class GetMedicineStockQuantityQueryHandler : IRequestHandler<GetMedicineStockQuantityQuery, double>
{
    private readonly IMedicineStockRepository _medicineStockRepository;

    public GetMedicineStockQuantityQueryHandler(IMedicineStockRepository medicineStockRepository)
    {
        _medicineStockRepository = medicineStockRepository;
    }

    public async Task<double> Handle(GetMedicineStockQuantityQuery request, CancellationToken cancellationToken)
    {
        return await _medicineStockRepository.GetMedicineStockQuantity(request, cancellationToken);
    }
}
