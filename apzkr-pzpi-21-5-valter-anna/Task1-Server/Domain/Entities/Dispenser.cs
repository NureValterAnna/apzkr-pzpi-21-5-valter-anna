
namespace Domain.Entities;

public class Dispenser : BaseEntity
{
    public string DispensorName { get; set; }

    public string Location { get; set;}

    public double StorageTemperature { get; set;}
}
