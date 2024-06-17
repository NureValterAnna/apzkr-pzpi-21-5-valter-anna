using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Dispensers.Commands.DeleteDispenser;

public class DeleteDispenserCommandHandler : IRequestHandler<DeleteDispenserCommand, int>
{
    private readonly IDispenserRepository _dispenserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteDispenserCommandHandler(IDispenserRepository dispenserRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _dispenserRepository = dispenserRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(DeleteDispenserCommand request, CancellationToken cancellationToken)
    {
        var removedDispenserId = await _dispenserRepository.DeleteDispenser(request, cancellationToken);
        return removedDispenserId;
    }
}
