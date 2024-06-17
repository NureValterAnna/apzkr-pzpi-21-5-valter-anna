
using Application.MedicineStocks.Commands.CreateMedicineStock;
using Application.MedicineStocks.Commands.DeleteMedicineStock;
using Application.MedicineStocks.Commands.UpdateMedicineStock;
using Application.MedicineStocks.Queries;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicineStockRepository : IRepository<MedicineStock>
{
    Task<int> CreateMedicineStock(CreateMedicineStockCommand request, CancellationToken cancellationToken);

    Task<double> GetMedicineStockQuantity(GetMedicineStockQuantityQuery request, CancellationToken cancellationToken);

    Task<int> UpdateMedicineStock(UpdateMedicineStockCommand request, CancellationToken cancellationToken);


    Task<int> DeleteMedicineStock(DeleteMedicineStockCommand request, CancellationToken cancellationToken);


}
