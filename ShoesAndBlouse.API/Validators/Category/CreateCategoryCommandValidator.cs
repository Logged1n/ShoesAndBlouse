using ShoesAndBlouse.Application.Categories.Commands;
using FluentValidation;

namespace ShoesAndBlouse.API.Validators.Category;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
       public CreateCategoryCommandValidator()
       {
              RuleFor(x => x.Name)
                     .NotEmpty().WithMessage("Category name is required.")
                     .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters");
                     //RuleFor(x => x.Products)
                     //.Must(p => p != null && p.Count != 0).WithMessage("Category must at least have one product");
       }
}