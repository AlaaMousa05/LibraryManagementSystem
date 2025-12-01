using FluentValidation;

namespace LibraryManagementSystem.DTOs.CategoryDTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }

    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must be at most 100 characters.");
        }
    }
}
