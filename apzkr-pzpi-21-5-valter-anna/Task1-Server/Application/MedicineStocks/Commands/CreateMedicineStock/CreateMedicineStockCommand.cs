using MediatR;

namespace Application.MedicineStocks.Commands.CreateMedicineStock;

public record CreateMedicineStockCommand : IRequest<int>
{
    public int MedicineId { get; set; }

    public int DispenserId { get; set; }

    public double Quantity { get; set; }
}
