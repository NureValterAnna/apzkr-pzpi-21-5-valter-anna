using AutoMapper;
using Domain.Entities;

namespace Application.MedicineStocks.Commands.DeleteMedicineStock;

public class DeleteMedicineStockProfile : Profile
{
    public DeleteMedicineStockProfile()
    {
        CreateMap<DeleteMedicineStockCommand, MedicineStock>();
    }
}
