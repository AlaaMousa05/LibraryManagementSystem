using LibraryManagementSystem.Entities;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task <Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<List<Book>> GetAllAsync();
}
