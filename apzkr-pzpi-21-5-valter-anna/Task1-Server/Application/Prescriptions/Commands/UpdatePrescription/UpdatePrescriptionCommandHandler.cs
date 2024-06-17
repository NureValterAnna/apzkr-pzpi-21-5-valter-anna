
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommandHandler : IRequestHandler<UpdatePrescriptionCommand, int>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _prescriptionRepository = prescriptionRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = _mapper.Map<Prescription>(request);
        await _prescriptionRepository.Update(prescription, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        return prescription.Id;
    }
}
