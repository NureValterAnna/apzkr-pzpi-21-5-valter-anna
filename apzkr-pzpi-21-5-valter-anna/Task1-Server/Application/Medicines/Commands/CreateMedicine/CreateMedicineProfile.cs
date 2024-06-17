using AutoMapper;
using Domain.Entities;

namespace Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineProfile : Profile
{
    public CreateMedicineProfile()
    {
        CreateMap<CreateMedicineCommand, Medicine>();
    }
}
