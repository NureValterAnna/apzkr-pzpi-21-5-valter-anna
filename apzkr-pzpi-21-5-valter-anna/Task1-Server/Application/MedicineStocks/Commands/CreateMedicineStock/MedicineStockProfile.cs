using AutoMapper;
using Domain.Entities;

namespace Application.MedicineStocks.Commands.CreateMedicineStock;

public class MedicineStockProfile : Profile
{
    public MedicineStockProfile()
    {
        CreateMap<CreateMedicineStockCommand, MedicineStock>();
    }
}
