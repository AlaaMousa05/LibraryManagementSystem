namespace LibraryManagementSystem.Entities
{
    public class Borrow
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime BorrowedAt { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; } 
    }

}
