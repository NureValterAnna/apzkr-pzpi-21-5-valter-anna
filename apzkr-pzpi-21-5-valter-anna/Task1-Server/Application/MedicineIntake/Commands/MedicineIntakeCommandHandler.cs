using Application.Medicines.Queries.GetMedicineByPrescription;
using Application.MedicineStocks.Commands.UpdateMedicineStock;
using Application.MedicineStocks.Queries;
using Application.Prescriptions.Queries;
using Application.Transactions.Commands.AddTransaction;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.MedicineIntake.Commands;

public class MedicineIntakeCommandHandler : IRequestHandler<MedicineIntakeCommand, double>
{
    private readonly IMediator _mediator;

    public MedicineIntakeCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<double> Handle(MedicineIntakeCommand request, CancellationToken cancellationToken)
    {
        var medicine = await _mediator.Send(new GetMedicineByPrescriptionQuery
        {
            PrescriptionId = request.PrescriptionId
        },
        cancellationToken);

        var quantity = await _mediator.Send(new GetMedicineStockQuantityQuery
        {
            DispenserId = request.DispenserId,
            MedicineId = medicine.Id
        },
        cancellationToken);

        var prescription = await _mediator.Send(new GetPrescriptionQuery
        {
            PrescriptionId = request.PrescriptionId
        });

        if (prescription.PrescriptionDateEnd > DateTime.UtcNow && prescription.PrescriptionDateStart < DateTime.UtcNow)
        {
            if (quantity > prescription.Dose)
            {
                await _mediator.Send(new AddTransactionCommand { PrescriptionId = request.PrescriptionId, TimesPerDay = prescription.TimesPerDay });

                await _mediator.Send(new UpdateMedicineStockCommand
                {
                    DispenserId = request.DispenserId,
                    MedicineId = prescription.MedicineId,
                    Quantity = quantity - prescription.Dose
                });

                return quantity - prescription.Dose;
            }
            else
            {
                throw new NotEnoughMedicineException();
            }
        }
        else
        {
            throw new PrescriptionHasExpiredException();
        }
    }
}
