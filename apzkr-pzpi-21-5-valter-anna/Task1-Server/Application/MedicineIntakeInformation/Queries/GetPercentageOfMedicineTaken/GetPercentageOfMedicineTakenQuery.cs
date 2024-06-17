using MediatR;

namespace Application.MedicineIntakeInformation.Queries.GetPercentageOfMedicineTaken;

public record GetPercentageOfMedicineTakenQuery : IRequest<double>
{
    public int PrescriptionId { get; set; }
}
