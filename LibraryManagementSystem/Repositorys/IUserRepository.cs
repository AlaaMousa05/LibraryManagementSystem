using LibraryManagementSystem.Entities;


namespace LibraryManagementSystem.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByIdAsync(string id);
        Task<AppUser?> GetByEmailAsync(string email);
        Task CreateAsync(AppUser user, string password);
        Task UpdateAsync(AppUser user);
    }
}
