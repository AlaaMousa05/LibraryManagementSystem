using FluentValidation;

namespace LibraryManagementSystem.DTOs.AuthorDTOs
{
    public class AuthorUpdateDto 
    {
        public string Name { get; set; } = null!;
    }

    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Author name is required.")
                .MaximumLength(200).WithMessage("Author name cannot exceed 200 characters.");
        }
    }
}
