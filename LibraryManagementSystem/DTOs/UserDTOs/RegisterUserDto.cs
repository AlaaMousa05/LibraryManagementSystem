using FluentValidation;

namespace LibraryManagementSystem.DTOs.UserDTOs
{
    public class RegisterUserDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
   
        public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
        {
            public RegisterUserDtoValidator()
            {
                RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("Full name is required")
                    .MinimumLength(3).WithMessage("Full name must be at least 3 characters");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters");
            }
        }
    }


