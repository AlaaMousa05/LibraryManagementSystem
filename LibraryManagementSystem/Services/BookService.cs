using LibraryManagementSystem.DTOs.BookDTOs;
using LibraryManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using System;
using LibraryManagementSystem.Exceptions;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(BookCreateDto request);
    Task<BookDto?> UpdateBookAsync(int id, BookUpdateDto request);
    Task<bool> DeleteBookAsync(int id);
    Task<bool> SetBookImage(int id, string imageUrl);
}


public class BookService : IBookService
{
    private readonly IBookRepository _repo;
    private readonly LibraryDbContext _context;

    public BookService(IBookRepository repo, LibraryDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var books = await _repo.GetAllAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Isbn = b.Isbn,
            CategoryId = b.CategoryId,
            PublicationYear = b.PublicationYear,
            Description = b.Description,
            CoverImageUrl = b.CoverImageUrl,
            AuthorIds = b.BookAuthors.Select(ba => ba.AuthorId).ToList()
        }).ToList();
    }

    public async Task<BookDto> GetBookByIdAsync(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null)
            throw new NotFoundException($"Book with ID {id} not found");

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            CategoryId = book.CategoryId,
            PublicationYear = book.PublicationYear,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList()
        };
    }

    public async Task<BookDto> CreateBookAsync(BookCreateDto request)
    {
        var book = new Book
        {
            Title = request.Title,
            Isbn = request.Isbn,
            CategoryId = request.CategoryId,
            PublicationYear = request.PublicationYear,
            Description = request.Description,
            CoverImageUrl = request.CoverImageUrl
        };

        foreach (var authorId in request.AuthorIds)
        {
            book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
        }

        var res = await _repo.AddAsync(book);

        return new BookDto
        {
            Id = res.Id,
            Title = res.Title,
            Isbn = res.Isbn,
            CategoryId = res.CategoryId,
            PublicationYear = res.PublicationYear,
            Description = res.Description,
            CoverImageUrl = res.CoverImageUrl,
            AuthorIds = res.BookAuthors.Select(ba => ba.AuthorId).ToList()
        };
    }

    public async Task<BookDto> UpdateBookAsync(int id, BookUpdateDto request)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null)
            throw new NotFoundException($"Book with ID {id} not found");

        if (!string.IsNullOrEmpty(request.Title)) book.Title = request.Title;
        if (!string.IsNullOrEmpty(request.Isbn)) book.Isbn = request.Isbn;
        if (request.PublicationYear.HasValue) book.PublicationYear = request.PublicationYear.Value;
        if (!string.IsNullOrEmpty(request.Description)) book.Description = request.Description;
        if (!string.IsNullOrEmpty(request.CoverImageUrl)) book.CoverImageUrl = request.CoverImageUrl;
        if (request.CategoryId.HasValue) book.CategoryId = request.CategoryId.Value;

        if (request.AuthorIds != null)
        {
            book.BookAuthors.Clear();
            foreach (var authorId in request.AuthorIds)
                book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
        }

        await _repo.UpdateAsync(book);

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            CategoryId = book.CategoryId,
            PublicationYear = book.PublicationYear,
            Description = book.Description,
            CoverImageUrl = book.CoverImageUrl,
            AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList()
        };
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null)
            throw new NotFoundException($"Book with ID {id} not found");

        await _repo.DeleteAsync(book);
        return true;
    }

    public async Task<bool> SetBookImage(int id, string imageUrl)
    {
        var book = await _repo.GetByIdAsync(id);
        if (book == null)
            throw new NotFoundException($"Book with ID {id} not found");

        book.CoverImageUrl = imageUrl;
        await _repo.UpdateAsync(book);
        return true;
    }
}
