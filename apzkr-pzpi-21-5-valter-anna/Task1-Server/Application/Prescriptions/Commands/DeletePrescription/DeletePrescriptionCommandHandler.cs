using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Prescriptions.Commands.DeletePrescription;

public class DeletePrescriptionCommandHandler : IRequestHandler<DeletePrescriptionCommand, int>
{
    private readonly IPrescriptionRepository _prescriptionRepositoty;
    private readonly IMapper _mapper;

    public DeletePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepositoty, IMapper mapper)
    {
        _prescriptionRepositoty = prescriptionRepositoty;
        _mapper = mapper;
    }

    public async Task<int> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var removedPrescriptionId = await _prescriptionRepositoty.DeletePrescription(request, cancellationToken);
        return removedPrescriptionId;
    }
}
