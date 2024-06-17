using MediatR;

namespace Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;

public record GetMedicineIntakeInformationQuery : IRequest<MedicineIntakeInformationResponse>
{
    public int PrescriptionId { get; set; }
}
