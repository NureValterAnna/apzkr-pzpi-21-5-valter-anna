
using Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;
using Application.MedicineIntakeInformation.Queries.GetPercentageOfMedicineTaken;
using Application.Medicines.Queries.GetMedicineByPrescription;
using Application.Prescriptions.Commands.CreatePrescription;
using Application.Prescriptions.Commands.DeletePrescription;
using Application.Prescriptions.Queries.GetPrescriptionsByEmail;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPrescriptionRepository : IRepository<Prescription>
{
    Task<int> CreatePrescription(CreatePrescriptionCommand request, CancellationToken cancellationToken);

    Task<int> DeletePrescription(DeletePrescriptionCommand request, CancellationToken cancellationToken);

    Task<Medicine> GetMedicineByPrescription(GetMedicineByPrescriptionQuery request, CancellationToken cancellationToken);

    Task<List<Prescription>> GetPrescriptionsByEmailAsync(GetPrescriptionsByEmailQuery request, CancellationToken cancellationToken);

    Task<MedicineIntakeInformationResponse> GetMedicineIntakeInformation(GetMedicineIntakeInformationQuery request, CancellationToken cancellationToken);

    Task<double> GetPercentageOfMedicineTaken(GetPercentageOfMedicineTakenQuery request,  CancellationToken cancellationToken);
}
