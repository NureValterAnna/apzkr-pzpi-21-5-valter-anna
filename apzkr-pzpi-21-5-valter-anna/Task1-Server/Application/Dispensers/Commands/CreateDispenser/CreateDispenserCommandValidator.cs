using FluentValidation;

namespace Application.Dispensers.Commands.CreateDispenser;

public class CreateDispenserCommandValidator : AbstractValidator<CreateDispenserCommand>
{
    public CreateDispenserCommandValidator()
    {
        RuleFor(x => x.DispensorName)
            .NotEmpty().WithMessage("Dispenser name is required.")
            .MaximumLength(100).WithMessage("Dispenser name cannot be longer than 100 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.");
    }
}
