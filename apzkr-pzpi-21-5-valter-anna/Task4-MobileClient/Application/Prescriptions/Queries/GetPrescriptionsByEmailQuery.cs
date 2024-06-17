namespace Application.Prescriptions.Queries;

public record GetPrescriptionsByEmailQuery()
{
    public string Email { get; set; }
}