using Application.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Prescriptions.Queries.GetPrescriptionsByEmail;

public class GetPrescriptionsByEmailQueryHandler : IRequestHandler<GetPrescriptionsByEmailQuery, List<PrescriptionResponse>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;

    public GetPrescriptionsByEmailQueryHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper)
    {
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
    }

    public async Task<List<PrescriptionResponse>> Handle(GetPrescriptionsByEmailQuery request, CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository.GetPrescriptionsByEmailAsync(request, cancellationToken);
        return _mapper.Map<List<PrescriptionResponse>>(prescriptions);
    }
}
