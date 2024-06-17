using MediatR;

namespace Application.MedicineStocks.Commands.UpdateMedicineStock;

public record UpdateMedicineStockCommand : IRequest<int>
{
    public int DispenserId {  get; set; }
    public int MedicineId { get; set; }
    public double Quantity { get; set; }
}
