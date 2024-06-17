using Application.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using MediatR;

namespace Application.Prescriptions.Queries;

public class GetPrescriptionQueryHandler : IRequestHandler<GetPrescriptionQuery, PrescriptionResponse>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;

    public GetPrescriptionQueryHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper)
    {
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
    }

    public async Task<PrescriptionResponse> Handle(GetPrescriptionQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetAsync(request.PrescriptionId, cancellationToken);
        return _mapper.Map<PrescriptionResponse>(prescription);
    }
}
