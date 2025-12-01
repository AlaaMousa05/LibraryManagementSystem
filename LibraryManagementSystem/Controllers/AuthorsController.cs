using LibraryManagementSystem.DTOs.AuthorDTOs;
using LibraryManagementSystem.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)},{nameof(RolesEnum.MEMBER)}")]
    public async Task<ActionResult<List<AuthorDto>>> GetAll()
    {
        var authors = await _service.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)},{nameof(RolesEnum.MEMBER)}")]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        var author = await _service.GetAuthorByIdAsync(id);
        return Ok(author); 
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<ActionResult<AuthorDto>> Create(AuthorCreateDto request)
    {
        var created = await _service.CreateAuthorAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<ActionResult<AuthorDto>> Update(int id, AuthorUpdateDto request)
    {
        var updated = await _service.UpdateAuthorAsync(id, request);
        return Ok(updated); 
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAuthorAsync(id);
        return NoContent();
    }
}
