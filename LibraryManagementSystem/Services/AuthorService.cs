using LibraryManagementSystem.DTOs.AuthorDTOs;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Exceptions;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAllAuthorsAsync();
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto request);
    Task<AuthorDto> UpdateAuthorAsync(int id, AuthorUpdateDto request);
    Task DeleteAuthorAsync(int id);
}

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repo;

    public AuthorService(IAuthorRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _repo.GetAllAsync();
        return authors.Select(a => new AuthorDto
        {
            Id = a.Id,
            Name = a.Name,
        }).ToList();
    }

    public async Task<AuthorDto> GetAuthorByIdAsync(int id)
    {
        var author = await _repo.GetByIdAsync(id);
        if (author == null)
            throw new NotFoundException($"Author with ID {id} not found");

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }

    public async Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto request)
    {
        var author = new Author
        {
            Name = request.Name
        };

        await _repo.AddAsync(author);

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }

    public async Task<AuthorDto> UpdateAuthorAsync(int id, AuthorUpdateDto request)
    {
        var author = await _repo.GetByIdAsync(id);
        if (author == null)
            throw new NotFoundException($"Author with ID {id} not found");

        author.Name = request.Name;
        await _repo.UpdateAsync(author);

        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Name,
        };
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _repo.GetByIdAsync(id);
        if (author == null)
            throw new NotFoundException($"Author with ID {id} not found");

        await _repo.DeleteAsync(author);
    }
}
