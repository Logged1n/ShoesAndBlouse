using FluentValidation;
using ShoesAndBlouse.API.Validators.ValueObjects;
using ShoesAndBlouse.Application.Products.Commands;

namespace ShoesAndBlouse.API.Validators.Product;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than zero.");

        When(x => !string.IsNullOrEmpty(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        });

        When(x => !string.IsNullOrEmpty(x.Description), () =>
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description cannot be empty.")
                .MaximumLength(1000).WithMessage("Product description cannot exceed 1000 characters.");
        });

        When(x => x.Price != null, () =>
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Product price cannot be null.")
                .SetValidator(
                    new MoneyValidator()!); // Validation for the Price property using the appropriate validator
        });

        //When(x => x.Category != null && x.Category.Count != 0, () =>
        //{
        //    RuleFor(x => x.Category)
       //         .Must(c => c != null && c.All(cat => !string.IsNullOrEmpty(cat))).WithMessage("Category names cannot be empty.");
       // });

        When(x => x.Photo != null, () =>
        {
            RuleFor(x => x.Photo)
                .NotNull().WithMessage("Product photo path cannot be null.");
        });
    }
}