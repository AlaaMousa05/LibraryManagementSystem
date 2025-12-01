using LibraryManagementSystem.Data;
using LibraryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<Author> _dbSet;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Author>();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _dbSet.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _dbSet.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAsync(Author author)
        {
            _dbSet.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
