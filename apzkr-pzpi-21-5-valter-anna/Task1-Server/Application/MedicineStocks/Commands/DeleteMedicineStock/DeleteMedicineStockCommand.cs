using MediatR;

namespace Application.MedicineStocks.Commands.DeleteMedicineStock;

public record DeleteMedicineStockCommand : IRequest<int>
{
    public int Id { get; set; }
}
