using Application.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Medicines.Queries.GetMedicines;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, List<MedicineResponse>>
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IMapper _mapper;

    public GetMedicinesQueryHandler(IMedicineRepository medicineRepository, IMapper mapper)
    {
        _medicineRepository = medicineRepository;
        _mapper = mapper;
    }

    public async Task<List<MedicineResponse>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var medicines = await _medicineRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<MedicineResponse>>(medicines);
    }
}
