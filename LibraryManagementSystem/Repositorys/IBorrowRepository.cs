using LibraryManagementSystem.Entities;


namespace LibraryManagementSystem.Repositories
{
    public interface IBorrowRepository 
    {
        Task<Borrow> AddAsync(Borrow borrow);
        Task<Borrow?> GetByIdAsync(int id);
        Task<List<Borrow>> GetAllAsync();
        Task<List<Borrow>> GetByUserIdAsync(string userId);
        Task UpdateAsync(Borrow borrow);
        Task DeleteAsync(Borrow borrow);


    }
}
