using FluentValidation;
namespace Application.Users.Commands.Register;

public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidation()
    {
        RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required");

        RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required")
                .GreaterThan(0).WithMessage("Age must be greater than 0");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Provided email is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(32).WithMessage("Password must not exceed 32 characters.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Password must contain at least one special symbol '!,?, *,.'.");
    }

}
