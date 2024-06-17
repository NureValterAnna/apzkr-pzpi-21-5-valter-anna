using FluentValidation;

namespace Application.Prescriptions.Commands.UpdatePrescription;

public class UpdatePrescriptionCommandValidator : AbstractValidator<UpdatePrescriptionCommand>
{
    public UpdatePrescriptionCommandValidator()
    {
        RuleFor(x => x.Dose).GreaterThanOrEqualTo(0).WithMessage("Morning dose cannot be negative.");

        RuleFor(x => x.TimesPerDay).GreaterThanOrEqualTo(0).WithMessage("Afternoon dose cannot be negative.");
    }
}
