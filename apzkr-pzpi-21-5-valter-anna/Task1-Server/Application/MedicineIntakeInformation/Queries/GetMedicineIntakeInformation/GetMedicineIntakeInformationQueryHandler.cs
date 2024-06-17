using Application.Interfaces;
using MediatR;

namespace Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;

public class GetMedicineIntakeInformationQueryHandler : IRequestHandler<GetMedicineIntakeInformationQuery, MedicineIntakeInformationResponse>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetMedicineIntakeInformationQueryHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<MedicineIntakeInformationResponse> Handle(GetMedicineIntakeInformationQuery request, CancellationToken cancellationToken)
    {
        return await _prescriptionRepository.GetMedicineIntakeInformation(request, cancellationToken);
    }
}
