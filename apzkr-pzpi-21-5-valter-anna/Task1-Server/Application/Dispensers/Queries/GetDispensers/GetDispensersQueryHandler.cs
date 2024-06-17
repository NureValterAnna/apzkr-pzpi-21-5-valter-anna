using Application.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using MediatR;

namespace Application.Dispensers.Queries.GetDispensers;

public class GetDispensersQueryHandler : IRequestHandler<GetDispensersQuery, List<DispenserResponse>>
{
    private readonly IDispenserRepository _dispenserRepository;
    private readonly IMapper _mapper;

    public GetDispensersQueryHandler(IDispenserRepository dispenserRepository, IMapper mapper)
    {
        _dispenserRepository = dispenserRepository;
        _mapper = mapper;
    }

    public async Task<List<DispenserResponse>> Handle(GetDispensersQuery request, CancellationToken cancellationToken)
    {
        var dispensers = await _dispenserRepository.GetAllAsync(request, cancellationToken);
        return _mapper.Map<List<DispenserResponse>>(dispensers);
    }
}
