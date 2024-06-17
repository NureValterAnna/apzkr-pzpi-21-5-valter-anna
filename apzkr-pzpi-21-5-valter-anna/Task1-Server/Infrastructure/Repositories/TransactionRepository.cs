using Application.Interfaces;
using Application.Transactions.Commands.AddTransaction;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public TransactionRepository(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> AddTransaction(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var todayUtc = DateTime.UtcNow.Date;
        var startOfDayUtc = new DateTime(todayUtc.Year, todayUtc.Month, todayUtc.Day, 0, 0, 0, DateTimeKind.Utc);
        var endOfDayUtc = new DateTime(todayUtc.Year, todayUtc.Month, todayUtc.Day, 23, 59, 59, DateTimeKind.Utc);
        var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.PrescriptionId == request.PrescriptionId && x.CreatedAt >= startOfDayUtc && x.CreatedAt <= endOfDayUtc);

        var newTransaction = _mapper.Map<Transaction>(request);
        if(transaction is not null)
        {
            if (transaction.TimesTaken < request.TimesPerDay)
            {
                transaction.TimesTaken += 1;
                await base.Update(transaction, cancellationToken);
            }
            else
            {
                throw new DailyDoseAlreadyTakenException();
            }
        }
        else
        {
            await base.Create(newTransaction);
        }
        await _unitOfWork.SaveChanges(cancellationToken);
        return newTransaction.Id;
    }
}
