using FluentValidation;
using ShoesAndBlouse.Application.Categories.Commands;

namespace ShoesAndBlouse.Application.Validators.Category;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be greater than zero.");
        
        When(x => !string.IsNullOrEmpty(x.Name), () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters");
        });

        //When(x => x.Products != null && x.Products.Count != 0, () =>
        //{
          //  RuleFor(x => x.Products)
            //    .NotNull().When(x => x.Products != null).WithMessage("Lista produktów nie może być pusta.")
              //  .Must(x => x != null && x.Count != 0).When(x => x.Products != null).WithMessage("Lista produktów nie może być pusta.");

        //});
    }
}