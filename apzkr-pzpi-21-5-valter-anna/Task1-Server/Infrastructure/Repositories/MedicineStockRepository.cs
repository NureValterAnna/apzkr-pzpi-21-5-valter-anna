using Application.Dispensers.Commands.CreateDispenser;
using Application.Interfaces;
using Application.MedicineStocks.Commands.CreateMedicineStock;
using Application.MedicineStocks.Commands.DeleteMedicineStock;
using Application.MedicineStocks.Commands.UpdateMedicineStock;
using Application.MedicineStocks.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Repositories;

public class MedicineStockRepository : Repository<MedicineStock>, IMedicineStockRepository
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public MedicineStockRepository(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateMedicineStock(CreateMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var medicineStock = await _context
            .MedicineStocks
            .Include(m => m.Dispenser)
            .Include(d => d.Medicine)
            .FirstOrDefaultAsync(x => x.DispenserId == request.DispenserId && x.MedicineId == request.MedicineId);

        if(medicineStock is not null)
        {
            throw new MedicineStockAlreadyExistsException();
        }
        var newMedicineStock = _mapper.Map<MedicineStock>(request);
        await base.Create(newMedicineStock);
        await _unitOfWork.SaveChanges(cancellationToken);
        return newMedicineStock.Id;
    }

    public async Task<double> GetMedicineStockQuantity(GetMedicineStockQuantityQuery request, CancellationToken cancellationToken)
    {
        var medicine = await _context.MedicineStocks.FirstOrDefaultAsync(x => x.DispenserId == request.DispenserId && x.MedicineId == request.MedicineId);
        if(medicine is null)
        {
            throw new MedicineStockDoesNotExistException();
        }
        return medicine?.Quantity ?? 0;
    }

    public async Task<int> UpdateMedicineStock(UpdateMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var medicine = await _context.MedicineStocks.FirstOrDefaultAsync(x => x.DispenserId == request.DispenserId && x.MedicineId == request.MedicineId);
        if (medicine is null)
        {
            throw new MedicineStockDoesNotExistException();
        }
        else
        {
            medicine.Quantity = request.Quantity;
            await base.Update(medicine, cancellationToken);
        }
        await _unitOfWork.SaveChanges(cancellationToken);
        return medicine.Id;
    }

    public async Task<int> DeleteMedicineStock(DeleteMedicineStockCommand request, CancellationToken cancellationToken)
    {
        var removedMedicineStock = await _context.MedicineStocks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (removedMedicineStock is null)
        {
            throw new MedicineStockDoesNotExistException();
        }
        await base.Delete(removedMedicineStock);
        return removedMedicineStock.Id;
    }
}
