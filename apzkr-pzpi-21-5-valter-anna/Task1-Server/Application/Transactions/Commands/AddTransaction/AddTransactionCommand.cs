using MediatR;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : IRequest<int>
{
    public int PrescriptionId { get; set; }

    public int TimesPerDay { get; set; }
}
