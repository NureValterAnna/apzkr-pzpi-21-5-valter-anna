using Application.Transactions.Commands.AddTransaction;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    public Task<int> AddTransaction(AddTransactionCommand request, CancellationToken cancellationToken);
}
