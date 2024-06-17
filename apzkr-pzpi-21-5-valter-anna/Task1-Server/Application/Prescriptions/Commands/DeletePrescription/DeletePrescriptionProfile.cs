using AutoMapper;
using Domain.Entities;

namespace Application.Prescriptions.Commands.DeletePrescription;

public class DeletePrescriptionProfile : Profile
{
    public DeletePrescriptionProfile()
    {
        CreateMap<DeletePrescriptionCommand, Prescription>();
    }
}
