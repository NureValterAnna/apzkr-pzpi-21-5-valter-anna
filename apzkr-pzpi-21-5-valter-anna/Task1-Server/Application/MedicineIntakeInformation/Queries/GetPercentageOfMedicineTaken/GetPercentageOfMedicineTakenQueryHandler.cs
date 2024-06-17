using Application.Interfaces;
using Application.Medicines.Queries.GetMedicineByPrescription;
using MediatR;

namespace Application.MedicineIntakeInformation.Queries.GetPercentageOfMedicineTaken;

public class GetPercentageOfMedicineTakenQueryHandler : IRequestHandler<GetPercentageOfMedicineTakenQuery, double>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetPercentageOfMedicineTakenQueryHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<double> Handle(GetPercentageOfMedicineTakenQuery request, CancellationToken cancellationToken)
    {
        return await _prescriptionRepository.GetPercentageOfMedicineTaken(request, cancellationToken);
    }
}
