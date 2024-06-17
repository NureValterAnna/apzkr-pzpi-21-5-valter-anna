using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public int PrescriptionId { get; set; }
    public Prescription Prescription { get; set; }

    public int TimesTaken { get; set; } = 1;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
