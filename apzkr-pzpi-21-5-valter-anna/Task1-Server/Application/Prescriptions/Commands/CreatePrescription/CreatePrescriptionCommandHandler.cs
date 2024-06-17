
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, int>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;

    public CreatePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper)
    {
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var newPrescriptionId = await _prescriptionRepository.CreatePrescription(request, cancellationToken);
        return newPrescriptionId;
    }
}
