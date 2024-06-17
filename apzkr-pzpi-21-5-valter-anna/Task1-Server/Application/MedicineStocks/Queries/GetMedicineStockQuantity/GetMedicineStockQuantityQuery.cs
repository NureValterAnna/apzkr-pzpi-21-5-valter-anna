using MediatR;

namespace Application.MedicineStocks.Queries;

public record GetMedicineStockQuantityQuery : IRequest<double>
{
    public int DispenserId { get; set; }

    public int MedicineId { get; set; }
}
