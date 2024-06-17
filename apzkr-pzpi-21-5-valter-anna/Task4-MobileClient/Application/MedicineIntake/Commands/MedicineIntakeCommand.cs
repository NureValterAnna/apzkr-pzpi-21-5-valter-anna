namespace Application.MedicineIntake.Commands;

public record MedicineIntakeCommand()
{
    public int DispenserId { get; set; }
    public int PrescriptionId { get; set; }
}