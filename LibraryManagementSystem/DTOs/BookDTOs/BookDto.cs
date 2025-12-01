namespace LibraryManagementSystem.DTOs.BookDTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int CategoryId { get; set; }
        public int PublicationYear { get; set; }
        public string? Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public List<int> AuthorIds { get; set; } = new(); 
    }
}
