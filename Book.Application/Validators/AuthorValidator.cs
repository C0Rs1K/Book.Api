using Book.Application.Dtos.AuthorDtos;
using FluentValidation;

namespace Book.Application.Validators;

public class AuthorValidator : AbstractValidator<AuthorRequestDto>
{
    public AuthorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.")
            .Must(BeAValidDate).WithMessage("Birth date is not a valid date.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date <= DateTime.Now;
    }
}