namespace LibraryManagementSystem.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PublicationYear { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<BookAuthor> BookAuthors { get; set; } = new();
        public List<Borrow> Borrows { get; set; } = new();
    }

}
