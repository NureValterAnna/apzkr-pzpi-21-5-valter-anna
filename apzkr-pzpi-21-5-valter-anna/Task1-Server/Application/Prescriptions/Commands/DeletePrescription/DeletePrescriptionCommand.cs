using MediatR;

namespace Application.Prescriptions.Commands.DeletePrescription;

public record DeletePrescriptionCommand : IRequest<int>
{
    public int Id { get; set; }
}
