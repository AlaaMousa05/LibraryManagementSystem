using FluentValidation;

namespace LibraryManagementSystem.DTOs.CategoryDTOs
{
    public class CategoryUpdateDto 
    {
        public string Name { get; set; }
    }
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must be at most 100 characters.");
        }
    }

}
