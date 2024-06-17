namespace Domain.Entities;

public class MedicineStock
{
    public int Id { get; set; }

    public int DispenserId { get; set; }

    public string? DispenserName { get; set; }

    public int MedicineId { get; set; }

    public string? MedicineTitle { get; set; }

    public double Quantity { get; set; }
}