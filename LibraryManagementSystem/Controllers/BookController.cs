using LibraryManagementSystem.DTOs.BookDTOs;
using LibraryManagementSystem.Enums;
using LibraryManagementSystem.Exceptions; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _service;
    public BooksController(BookService service) => _service = service;

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllBooksAsync());

    [HttpGet("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)},{nameof(RolesEnum.MEMBER)}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> Create([FromBody] BookCreateDto request)
    {
        var book = await _service.CreateBookAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto request)
    {
        var book = await _service.UpdateBookAsync(id, request);
        return Ok(book);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteBookAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/images")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> SetImage(int id, [FromBody] string imageUrl)
    {
        await _service.SetBookImage(id, imageUrl);
        return Ok();
    }
}
