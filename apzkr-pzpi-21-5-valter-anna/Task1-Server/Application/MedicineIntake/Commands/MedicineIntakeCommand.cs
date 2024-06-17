using MediatR;

namespace Application.MedicineIntake.Commands;

public record MedicineIntakeCommand : IRequest<double>
{
    public int DispenserId { get; set; }
    public int PrescriptionId { get; set; }
}
