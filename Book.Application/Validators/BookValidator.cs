using Book.Application.Dtos.BookDtos;
using FluentValidation;

namespace Book.Application.Validators;

public class BookValidator : AbstractValidator<BookRequestDto>
{
    public BookValidator()
    {
        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$").WithMessage("ISBN is not valid.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required.")
            .MaximumLength(50).WithMessage("Genre cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.TakeTime)
            .NotEmpty().WithMessage("Take time is required.")
            .Must(BeAValidTakeTime).WithMessage("Take time must be a valid date and time.");

        RuleFor(x => x.ReturnTime)
            .NotEmpty().WithMessage("Return time is required.")
            .Must((dto, returnTime) => BeAValidReturnTime(dto.TakeTime, returnTime)).WithMessage("Return time must be a valid date and time.");

        RuleFor(x => x.BookOwner)
            .NotEmpty().WithMessage("Book owner is required.")
            .MaximumLength(100).WithMessage("Book owner name cannot exceed 100 characters.");
    }

    private bool BeAValidTakeTime(DateTime takeTime)
    {
        return takeTime >= DateTime.Now;
    }

    private bool BeAValidReturnTime(DateTime takeTime, DateTime returnTime)
    {
        return returnTime > takeTime;
    }
}