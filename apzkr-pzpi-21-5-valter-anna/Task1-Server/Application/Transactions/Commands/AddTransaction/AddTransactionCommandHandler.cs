using Application.Interfaces;
using Application.Users.Queries.GetUserByEmail;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Transactions.Commands.AddTransaction;

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public AddTransactionCommandHandler(IMapper mapper, ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var newTransactionId = await _transactionRepository.AddTransaction(request, cancellationToken);
        return newTransactionId;
    }
}
