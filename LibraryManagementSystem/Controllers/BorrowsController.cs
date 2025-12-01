using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.BorrowDTOs;
using LibraryManagementSystem.Enums;

[ApiController]
[Route("api/[controller]")]
public class BorrowsController : ControllerBase
{
    private readonly IBorrowService _service;

    public BorrowsController(IBorrowService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = nameof(RolesEnum.MEMBER))]
    public async Task<IActionResult> CreateBorrow(BorrowCreateDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
        var result = await _service.CreateBorrow(userId, dto.BookId);
        return Ok(result); 
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> GetBorrow(int id)
    {
        var result = await _service.GetBorrow(id); 
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var result = await _service.GetByUserId(userId); 
        return Ok(result);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> UpdateStatus(int id, BorrowUpdateStatusDto dto)
    {
        var updated = await _service.UpdateStatus(id, dto.Status);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}
