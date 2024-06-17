using Application.Medicines.Queries.GetMedicines;
using MediatR;

namespace Application.Medicines.Queries.GetMedicineByPrescription;

public record GetMedicineByPrescriptionQuery : IRequest<MedicineResponse>
{
    public int PrescriptionId { get; set; }
}
