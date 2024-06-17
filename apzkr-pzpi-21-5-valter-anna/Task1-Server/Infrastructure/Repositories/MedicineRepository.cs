using Application.Dispensers.Commands.CreateDispenser;
using Application.Interfaces;
using Application.Medicines.Commands.CreateMedicine;
using Application.Medicines.Commands.DeleteMedicine;
using Application.Medicines.Queries.GetMedicineByPrescription;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Repositories;

public class MedicineRepository : Repository<Medicine>, IMedicineRepository
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;


    public MedicineRepository(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> CreateMedicine(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Title == request.Title, cancellationToken);
        if (medicine is not null)
        {
            throw new MedicinesAlreadyExistsException();
        }
        var newMedicine = _mapper.Map<Medicine>(request);
        await base.Create(newMedicine);
        await _unitOfWork.SaveChanges(cancellationToken);
        return newMedicine.Id;
    }

    public async Task<int> DeleteMedicine(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var removedMedicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (removedMedicine is null)
        {
            throw new MedicineNotFoundException();
        }
        await base.Delete(removedMedicine);
        return removedMedicine.Id;
    }

    public override Task<List<Medicine>> GetAllAsync(CancellationToken cancellationToken)
    {
        var user = base.GetAllAsync(cancellationToken);
        return user;
    }
}
