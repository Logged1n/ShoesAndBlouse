using FluentValidation;
using ShoesAndBlouse.Application.Reviews.Commands;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");
        RuleFor(x => x.Score).InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        // Inne reguły walidacji
    }
}