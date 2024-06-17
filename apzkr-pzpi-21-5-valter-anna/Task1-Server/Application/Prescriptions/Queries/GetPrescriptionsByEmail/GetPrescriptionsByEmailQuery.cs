using MediatR;

namespace Application.Prescriptions.Queries.GetPrescriptionsByEmail;

public record GetPrescriptionsByEmailQuery : IRequest<List<PrescriptionResponse>>
{
    public string Email { get; set; }
}
