namespace Domain.Entities;

public class Prescription
{
    public int Id { get; set; }
    
    public int MedicineId { get; set; }
    
    public Medicine Medicine { get; set; } = null!;
    
    public double Dose { get; set; }

    public int TimesPerDay { get; set; }
}