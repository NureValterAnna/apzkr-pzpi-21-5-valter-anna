namespace Domain.Entities;

public class MedicineStock : BaseEntity
{
    public int MedicineId {  get; set; }
    public Medicine Medicine { get; set; }

    public int DispenserId { get; set; }
    public Dispenser Dispenser { get; set; }

    public double Quantity { get; set; }
}
