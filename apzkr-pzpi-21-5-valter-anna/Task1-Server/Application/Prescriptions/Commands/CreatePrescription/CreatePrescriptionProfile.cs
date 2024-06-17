using AutoMapper;
using Domain.Entities;

namespace Application.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionProfile : Profile
{
    public CreatePrescriptionProfile()
    {
        CreateMap<CreatePrescriptionCommand, Prescription>();
    }
}
