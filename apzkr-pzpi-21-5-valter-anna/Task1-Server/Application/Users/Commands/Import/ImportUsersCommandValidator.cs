using FluentValidation;

namespace Application.Users.Commands.Import;

public class ImportUsersCommandValidator : AbstractValidator<ImportUsersCommand>
{
    public ImportUsersCommandValidator()
    {
        RuleFor(x => x.Json)
            .NotEmpty().WithMessage("Json is required.");
    }
}
