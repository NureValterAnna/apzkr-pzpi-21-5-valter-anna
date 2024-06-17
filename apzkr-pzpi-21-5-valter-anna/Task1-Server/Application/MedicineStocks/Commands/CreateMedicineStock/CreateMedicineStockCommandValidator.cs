using FluentValidation;

namespace Application.MedicineStocks.Commands.CreateMedicineStock;

public class CreateMedicineStockCommandValidator : AbstractValidator<CreateMedicineStockCommand>
{
    public CreateMedicineStockCommandValidator()
    {
        RuleFor(x => x.MedicineId)
            .NotEmpty().WithMessage("MedicineId is required.")
            .GreaterThan(0).WithMessage("MedicineId must be greater than zero.");

        RuleFor(x => x.DispenserId)
            .NotEmpty().WithMessage("DispenserId is required.")
            .GreaterThan(0).WithMessage("DispenserId must be greater than zero.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
