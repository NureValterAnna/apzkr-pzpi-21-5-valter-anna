using Application.Interfaces;
using Application.Medicines.Queries.GetMedicines;
using AutoMapper;
using MediatR;

namespace Application.Medicines.Queries.GetMedicineByPrescription;

public class GetMedicineByPrescriptionQueryHandler : IRequestHandler<GetMedicineByPrescriptionQuery, MedicineResponse>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;
    public GetMedicineByPrescriptionQueryHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper)
    {
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
    }

    public async Task<MedicineResponse> Handle(GetMedicineByPrescriptionQuery request, CancellationToken cancellationToken)
    {
        var medicine = await _prescriptionRepository.GetMedicineByPrescription(request, cancellationToken);
        return _mapper.Map<MedicineResponse>(medicine);
    }
}
