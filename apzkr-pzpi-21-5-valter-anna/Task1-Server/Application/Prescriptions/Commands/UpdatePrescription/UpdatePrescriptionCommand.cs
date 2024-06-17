using Domain.Entities;
using MediatR;

namespace Application.Prescriptions.Commands.UpdatePrescription;

public record UpdatePrescriptionCommand : IRequest<int>
{
    public int Id { get; set; }

    public double Dose { get; set; }

    public int TimesPerDay { get; set; }
}
