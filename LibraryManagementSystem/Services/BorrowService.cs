using LibraryManagementSystem.DTOs.BorrowDTOs;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Enums;
using LibraryManagementSystem.Exceptions;

namespace LibraryManagementSystem.Services
{
    public interface IBorrowService
    {
        Task<BorrowDto> CreateBorrow(string userId, int bookId);
        Task<BorrowDto> GetBorrow(int id);
        Task<List<BorrowDto>> GetAll();
        Task<List<BorrowDto>> GetByUserId(string userId);
        Task<BorrowDto> UpdateStatus(int id, string status);
        Task Delete(int id);
    }

    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _repo;

        public BorrowService(IBorrowRepository repo)
        {
            _repo = repo;
        }

        public async Task<BorrowDto> CreateBorrow(string userId, int bookId)
        {
            var borrow = new Borrow
            {
                UserId = userId,
                BookId = bookId,
                BorrowedAt = DateTime.Now,
                DueAt = DateTime.Now.AddDays(14),
                Status = BorrowStatus.Borrowed.ToString()
            };

            await _repo.AddAsync(borrow);
            return ToDto(borrow);
        }

        public async Task<BorrowDto> GetBorrow(int id)
        {
            var borrow = await _repo.GetByIdAsync(id);
            if (borrow == null)
                throw new NotFoundException($"Borrow record with ID {id} not found");

            return ToDto(borrow);
        }

        public async Task<List<BorrowDto>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ToDto).ToList();
        }

        public async Task<List<BorrowDto>> GetByUserId(string userId)
        {
            var list = await _repo.GetByUserIdAsync(userId);
            if (list == null || list.Count == 0)
                throw new NotFoundException($"No borrow records found for user {userId}");

            return list.Select(ToDto).ToList();
        }

        public async Task<BorrowDto> UpdateStatus(int id, string status)
        {
            var borrow = await _repo.GetByIdAsync(id);
            if (borrow == null)
                throw new NotFoundException($"Borrow record with ID {id} not found");

            borrow.Status = status;
            if (status == BorrowStatus.Returned.ToString())
                borrow.ReturnedAt = DateTime.Now;

            await _repo.UpdateAsync(borrow);
            return ToDto(borrow);
        }

        public async Task Delete(int id)
        {
            var borrow = await _repo.GetByIdAsync(id);
            if (borrow == null)
                throw new NotFoundException($"Borrow record with ID {id} not found");

            await _repo.DeleteAsync(borrow);
        }

        private static BorrowDto ToDto(Borrow b)
        {
            return new BorrowDto
            {
                Id = b.Id,
                UserId = b.UserId,
                UserName = b.User?.UserName ?? "",
                BookId = b.BookId,
                BookTitle = b.Book?.Title ?? "",
                BorrowedAt = b.BorrowedAt,
                DueAt = b.DueAt,
                ReturnedAt = b.ReturnedAt,
                Status = b.Status
            };
        }
    }
}
