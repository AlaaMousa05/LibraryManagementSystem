using LibraryManagementSystem.Entities;


namespace LibraryManagementSystem.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(Author author);
    }
}
