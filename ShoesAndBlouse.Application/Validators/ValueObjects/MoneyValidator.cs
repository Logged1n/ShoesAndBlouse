using FluentValidation;
using ShoesAndBlouse.Domain.ValueObjects;

namespace ShoesAndBlouse.API.Validators.ValueObjects;

public class MoneyValidator : AbstractValidator<Money>
{
    public MoneyValidator()
    {
        When(x => x.Currency != string.Empty, () =>
        {
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .MaximumLength(3).WithMessage("Currency cannot exceed 3 characters.");
        });
        When(x => x.Amount is { } @decimal, () =>
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        });
    }
}