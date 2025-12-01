using LibraryManagementSystem.Data;
using LibraryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Borrow> AddAsync(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();
            return borrow;
        }

        public async Task<Borrow?> GetByIdAsync(int id)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Borrow>> GetAllAsync()
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<List<Borrow>> GetByUserIdAsync(string userId)
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Borrow borrow)
        {
            _context.Borrows.Remove(borrow);
            await _context.SaveChangesAsync();
        }
    }

}
