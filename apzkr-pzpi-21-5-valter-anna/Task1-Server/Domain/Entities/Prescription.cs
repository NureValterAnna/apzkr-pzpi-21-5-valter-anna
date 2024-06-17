using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Prescription : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime PrescriptionDateStart { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime PrescriptionDateEnd { get; set; }

    public double Dose { get; set; }

    public int TimesPerDay { get; set; }

    public List<Transaction> Transactions { get; set; }
}
