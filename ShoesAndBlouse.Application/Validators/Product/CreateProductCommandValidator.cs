using FluentValidation;
using ShoesAndBlouse.Application.Products.Commands;
using ShoesAndBlouse.Application.Validators.ValueObjects;

namespace ShoesAndBlouse.Application.Validators.Product;

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MaximumLength(500).WithMessage("Product description cannot exceed 500 characters.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Product price is required.")
                .SetValidator(new MoneyValidator()); // Validation for the Price property using the appropriate validator

            //RuleFor(x => x.CategoryIds)
                //.Must(c => c != null && c.Count != 0).WithMessage("Product must be assigned to at least one category.");

            //RuleFor(x => x.Photo)
               // .NotEmpty().WithMessage("Product photo is required.");
        }
    }
