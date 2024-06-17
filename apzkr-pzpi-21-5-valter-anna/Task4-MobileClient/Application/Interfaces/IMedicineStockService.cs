using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicineStockService
{
    Task<List<MedicineStock>> GetAllMedicineStocksAsync();
}