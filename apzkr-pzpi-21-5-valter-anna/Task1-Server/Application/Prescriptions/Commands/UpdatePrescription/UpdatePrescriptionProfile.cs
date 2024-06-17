using AutoMapper;
using Domain.Entities;

namespace Application.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionProfile : Profile
{
    public UpdatePrescriptionProfile()
    {
        CreateMap<UpdatePrescriptionCommand, Prescription>();
    }
}
