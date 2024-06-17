using Application.Interfaces;
using Application.MedicineIntakeInformation.Queries.GetMedicineIntakeInformation;
using Application.MedicineIntakeInformation.Queries.GetPercentageOfMedicineTaken;
using Application.Medicines.Queries.GetMedicineByPrescription;
using Application.Prescriptions.Commands.CreatePrescription;
using Application.Prescriptions.Commands.DeletePrescription;
using Application.Prescriptions.Queries;
using Application.Prescriptions.Queries.GetPrescriptionsByEmail;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.Repositories;

public class PrecriptionRepository : Repository<Prescription>, IPrescriptionRepository
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public PrecriptionRepository(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeletePrescription(DeletePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var removedPrescription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (removedPrescription is null)
        {
            throw new PrescriptionNotFoundException();
        }
        await base.Delete(removedPrescription);
        return removedPrescription.Id;
    }

    public async override Task Create(Prescription entity)
    {
        bool isIntersecting = await _context.Prescriptions
                .AnyAsync(p => p.UserId == entity.UserId &&
                               p.MedicineId == entity.MedicineId &&
                               ((p.PrescriptionDateStart <= entity.PrescriptionDateEnd && p.PrescriptionDateEnd >= entity.PrescriptionDateStart) ||
                                (entity.PrescriptionDateStart <= p.PrescriptionDateEnd && entity.PrescriptionDateEnd >= p.PrescriptionDateStart)));
        if(isIntersecting) 
        {
            throw new PrescriptionAlreadyExistsException();
        }
        await base.Create(entity);
    }

    public async Task<int> CreatePrescription(CreatePrescriptionCommand request, CancellationToken cancellationToken)
    {
        var newPrescription = _mapper.Map<Prescription>(request);
        bool isIntersecting = await _context.Prescriptions
                .AnyAsync(p => p.UserId == request.UserId &&
                               p.MedicineId == request.MedicineId &&
                               ((p.PrescriptionDateStart <= request.PrescriptionDateEnd && p.PrescriptionDateEnd >= request.PrescriptionDateStart) ||
                                (request.PrescriptionDateStart <= p.PrescriptionDateEnd && request.PrescriptionDateEnd >= p.PrescriptionDateStart)));
        if (isIntersecting)
        {
            throw new PrescriptionAlreadyExistsException();
        }
        await base.Create(newPrescription);
        await _unitOfWork.SaveChanges(cancellationToken);
        return newPrescription.Id;
    }

    public async override Task Update(Prescription entity, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);

        if (prescription is null)
        {
            throw new PrescriptionNotFoundException();
        }

        prescription.Dose = entity.Dose;


        await base.Update(prescription, cancellationToken);
    }

    public override Task<Prescription> GetAsync(int id, CancellationToken cancellationToken)
    {
        var prescription = base.GetAsync(id, cancellationToken);
        if (prescription.Result is null)
        {
            throw new PrescriptionNotFoundException();
        }
        return prescription;
    }

    public async Task<Medicine> GetMedicineByPrescription(GetMedicineByPrescriptionQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions
            .Include(p => p.Medicine)
            .FirstOrDefaultAsync(x => x.Id == request.PrescriptionId, cancellationToken);

        if (prescription is null)
        {
            throw new PrescriptionNotFoundException();
        }

        return prescription.Medicine;
    }

    public async Task<List<Prescription>> GetPrescriptionsByEmailAsync(GetPrescriptionsByEmailQuery request, CancellationToken cancellationToken)
    {
        var prescriptions = await _context.Prescriptions
            .Include(p => p.User)
            .Include(p => p.Medicine)
            .Where(p => p.User.Email == request.Email)
            .ToListAsync();

        return prescriptions;
    }

    public async Task<MedicineIntakeInformationResponse> GetMedicineIntakeInformation(GetMedicineIntakeInformationQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.Id == request.PrescriptionId, cancellationToken);
        
        if(prescription is not null)
        {
            var medicineIntakeInformation = await _context.Users
            .Join(_context.Prescriptions,
            user => user.Id,
            prescription => prescription.UserId,
            (user, prescription) => new
            {
                User = user,
                Prescription = prescription
            })
            .Join(_context.Transactions,
            firstJoinResult => firstJoinResult.Prescription.Id,
            transaction => transaction.PrescriptionId,
            (firstJoinResult, transaction) => new
            {
                User = firstJoinResult.User,
                Prescription = firstJoinResult.Prescription,
                Transaction = transaction
            })
            .Where(secondJoinResult => secondJoinResult.Prescription.Id == request.PrescriptionId)
            .GroupBy(result => new
            {
                result.User.Name,
                result.User.Surname,
                result.Prescription.TimesPerDay,
                result.Prescription.PrescriptionDateStart
            })
            .Select(grouped => new
            {
                Name = grouped.Key.Name,
                Surname = grouped.Key.Surname,
                TotalTakenDoses = grouped.Sum(x => x.Transaction.TimesTaken),
                ExpectedTotalDoses = grouped.Sum(x => x.Prescription.TimesPerDay * (DateTime.UtcNow - x.Prescription.PrescriptionDateStart).Days + 2),
                MissedDoses = grouped.Sum(x => x.Prescription.TimesPerDay * (DateTime.UtcNow - x.Prescription.PrescriptionDateStart).Days + 2) - grouped.Sum(x => x.Transaction.TimesTaken)
            })
            .FirstOrDefaultAsync();

            if (medicineIntakeInformation != null)
            {
                var result = new MedicineIntakeInformationResponse
                {
                    Name = medicineIntakeInformation.Name,
                    Surname = medicineIntakeInformation.Surname,
                    TotalTakenDoses = medicineIntakeInformation.TotalTakenDoses,
                    ExpectedTotalDoses = medicineIntakeInformation.ExpectedTotalDoses,
                    MissedDoses = medicineIntakeInformation.MissedDoses
                };
                return result;
            }
            else
            {
                var result = new MedicineIntakeInformationResponse
                {
                    Name = null,
                    Surname = null,
                    TotalTakenDoses = 0,
                    ExpectedTotalDoses = 0,
                    MissedDoses = 0
                };
                return result;
            }
        }
        else
        {
            throw new PrescriptionNotFoundException();
        }
        
    }

    public async Task<double> GetPercentageOfMedicineTaken(GetPercentageOfMedicineTakenQuery request, CancellationToken cancellationToken)
    {
        var prescription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.Id == request.PrescriptionId, cancellationToken);

        if (prescription is not null)
        {
            var medicineIntakeInformation = await _context.Prescriptions
            .Join(_context.Transactions,
            prescription => prescription.Id,
            transaction => transaction.PrescriptionId,
            (prescription, transaction) => new
            {
                Prescription = prescription,
                Transaction = transaction
            })
            .Where(joinResult => joinResult.Prescription.Id == request.PrescriptionId)
            .GroupBy(result => new
            {
                result.Prescription.TimesPerDay,
                result.Prescription.PrescriptionDateStart,
                result.Prescription.PrescriptionDateEnd
            })
            .Select(grouped => new
            {
                TotalTakenDoses = grouped.Sum(x => x.Transaction.TimesTaken),
                ExpectedTotalDoses = grouped.Sum(x => x.Prescription.TimesPerDay * (x.Prescription.PrescriptionDateEnd - x.Prescription.PrescriptionDateStart).Days + 2),
                MissedDoses = grouped.Sum(x => x.Prescription.TimesPerDay * (x.Prescription.PrescriptionDateEnd - x.Prescription.PrescriptionDateStart).Days + 2) - grouped.Sum(x => x.Transaction.TimesTaken)
            })
            .FirstOrDefaultAsync();

            if(medicineIntakeInformation is null)
            {
                return 0;
            }

            double percentTaken = (double)medicineIntakeInformation.TotalTakenDoses / medicineIntakeInformation.ExpectedTotalDoses * 100;
            return percentTaken;
            
        }
        else
        {
            throw new PrescriptionNotFoundException();
        }
    }
}
