namespace Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;

public class MedicineIntakeInformationResponse
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public int TotalTakenDoses { get; set; }

    public int ExpectedTotalDoses { get; set; }

    public int MissedDoses { get; set; }
}
