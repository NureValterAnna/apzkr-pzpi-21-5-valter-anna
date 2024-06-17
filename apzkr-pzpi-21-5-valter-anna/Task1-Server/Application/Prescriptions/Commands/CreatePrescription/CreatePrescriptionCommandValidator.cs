
using FluentValidation;

namespace Application.Prescriptions.Commands.CreatePrescription;

public class CreatePrescriptionCommandValidator : AbstractValidator<CreatePrescriptionCommand>
{
    public CreatePrescriptionCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .WithMessage("User Id is required.");

        RuleFor(x => x.MedicineId).NotEmpty()
            .WithMessage("Medicine Id is required.");

        RuleFor(x => x.PrescriptionDateStart)
            .NotEmpty().WithMessage("Prescription start date is required.")
            .LessThanOrEqualTo(x => x.PrescriptionDateEnd).WithMessage("Prescription start date must be less than or equal to end date.");

        RuleFor(x => x.PrescriptionDateEnd)
            .NotEmpty().WithMessage("Prescription end date is required.")
            .GreaterThanOrEqualTo(x => x.PrescriptionDateStart).WithMessage("Prescription end date must be greater than or equal to start date.");

        RuleFor(x => x.Dose).GreaterThanOrEqualTo(0).WithMessage("Evening dose cannot be negative.");

        RuleFor(x => x.TimesPerDay).GreaterThanOrEqualTo(0).WithMessage("Evening dose cannot be negative.");

    }
}
