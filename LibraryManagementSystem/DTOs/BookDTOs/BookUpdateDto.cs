using FluentValidation;

namespace LibraryManagementSystem.DTOs.BookDTOs
{
    public class BookUpdateDto
    {
        public string? Title { get; set; }
        public string? Isbn { get; set; }
        public int? PublicationYear { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public List<int>? AuthorIds { get; set; } = new();
    }

    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(x => x.Title).MaximumLength(256);
            RuleFor(x => x.Isbn).MaximumLength(20);
            RuleFor(x => x.PublicationYear).InclusiveBetween(1500, DateTime.Now.Year)
                .When(x => x.PublicationYear.HasValue);
            RuleFor(x => x.CategoryId).GreaterThan(0)
                .When(x => x.CategoryId.HasValue);
        }
    }
}
