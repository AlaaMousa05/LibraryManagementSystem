namespace LibraryManagementSystem.Entities

{
    using Microsoft.AspNetCore.Identity;

    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? ProfileImageUrl { get; set; }

       
        public List<Borrow> Borrows { get; set; } = new();
    }

}
