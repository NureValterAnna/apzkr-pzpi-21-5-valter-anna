using FluentValidation;

namespace Application.Users.Commands.ChangeRole;

public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
{
    public ChangeRoleCommandValidator()
    {
        RuleFor(x => x.NewRole)
            .Must(role => IsValidRole(role))
            .WithMessage("New role must be: admin, patient, doctor");
    }

    private bool IsValidRole(string role)
    {
        return role == "admin" || role == "patient" || role == "doctor";
    }
}
