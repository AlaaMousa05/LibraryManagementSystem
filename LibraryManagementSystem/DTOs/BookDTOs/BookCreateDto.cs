using FluentValidation;

namespace LibraryManagementSystem.DTOs.BookDTOs
{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PublicationYear { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public int CategoryId { get; set; }
        public List<int> AuthorIds { get; set; } = new();
    }

    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Isbn).NotEmpty().MaximumLength(20);
            RuleFor(x => x.PublicationYear).InclusiveBetween(1500, DateTime.Now.Year);
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.AuthorIds).NotEmpty().WithMessage("Book must have at least one author.");
        }
    }

}
