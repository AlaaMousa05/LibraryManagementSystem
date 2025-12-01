namespace LibraryManagementSystem.DTOs.BorrowDTOs
{
    public class BorrowUpdateDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime DueAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Status { get; set; }
    }
}
