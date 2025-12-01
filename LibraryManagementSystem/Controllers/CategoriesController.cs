using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.CategoryDTOs;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Enums;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)},{nameof(RolesEnum.MEMBER)}")]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)},{nameof(RolesEnum.MEMBER)}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<ActionResult<CategoryDto>> Create(CategoryCreateDto dto)
    {
        var result = await _service.CreateAsync(dto); 
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<ActionResult<CategoryDto>> Update(int id, CategoryUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto); 
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id); 
        return NoContent();
    }
}
