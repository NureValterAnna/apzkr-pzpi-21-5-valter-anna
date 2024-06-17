using AutoMapper;
using Domain.Entities;

namespace Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineProfile : Profile
{
    public DeleteMedicineProfile()
    {
        CreateMap<DeleteMedicineCommand, Medicine>();
    }
}
