using MediatR;

namespace Application.Prescriptions.Queries;

public record GetPrescriptionQuery : IRequest<PrescriptionResponse>
{
    public int PrescriptionId { get; set; }
}
