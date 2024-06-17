using AutoMapper;
using Domain.Entities;

namespace Application.Transactions.Commands.AddTransaction;

public class AddTransactionProfile : Profile
{
    public AddTransactionProfile()
    {
        CreateMap<AddTransactionCommand, Transaction>();
    }
}
