using MediatR;

namespace Application.Prescriptions.Commands.CreatePrescription;

public record CreatePrescriptionCommand : IRequest<int>
{
    public int UserId { get; set; }

    public int MedicineId { get; set; }

    public DateTime PrescriptionDateStart { get; set; }

    public DateTime PrescriptionDateEnd { get; set; }

    public double Dose { get; set; }

    public int TimesPerDay { get; set; }

}
