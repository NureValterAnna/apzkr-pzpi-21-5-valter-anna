using Application.Medicines.Commands.CreateMedicine;
using Application.Medicines.Commands.DeleteMedicine;
using Application.Medicines.Queries.GetMedicineByPrescription;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMedicineRepository : IRepository<Medicine>
{
    Task<int> CreateMedicine(CreateMedicineCommand request, CancellationToken cancellationToken);

    Task<int> DeleteMedicine(DeleteMedicineCommand request, CancellationToken cancellationToken);


}
