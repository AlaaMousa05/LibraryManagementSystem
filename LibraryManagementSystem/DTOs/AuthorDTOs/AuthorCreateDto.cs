using FluentValidation;

namespace LibraryManagementSystem.DTOs.AuthorDTOs
{
    public class AuthorCreateDto
    {
        public string Name { get; set; } = null!;
    }

    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(200).WithMessage("Author name cannot exceed 200 characters.");
        }
    }
}
