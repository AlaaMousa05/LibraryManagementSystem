using LibraryManagementSystem.Data;
using LibraryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context) => _context = context;

        public async Task <Book> AddAsync(Book book)
        {
            var entity= await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync() =>
            await _context.Books.Include(b => b.BookAuthors)
                                .ThenInclude(ba => ba.Author)
                                .AsNoTracking()
                                .ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) =>
            await _context.Books.Include(b => b.BookAuthors)
                                .ThenInclude(ba => ba.Author)
                                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
