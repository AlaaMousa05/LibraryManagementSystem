using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.UserDTOs;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Enums;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-member")]
        public async Task<IActionResult> RegisterMember(RegisterUserDto dto)
        {
            await _userService.RegisterAsync(dto, RolesEnum.MEMBER.ToString());
            return Ok("Member registered successfully");
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = nameof(RolesEnum.ADMIN))]
        public async Task<IActionResult> RegisterAdmin(RegisterUserDto dto)
        {
            await _userService.RegisterAsync(dto, RolesEnum.ADMIN.ToString());
            return Ok("Admin registered successfully");
        }

        [HttpPost("register-librarian")]
        [Authorize(Roles = $"{nameof(RolesEnum.ADMIN)},{nameof(RolesEnum.LIBRARIAN)}")]
        public async Task<IActionResult> RegisterLibrarian(RegisterUserDto dto)
        {
            await _userService.RegisterAsync(dto, RolesEnum.LIBRARIAN.ToString());
            return Ok("Librarian registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            return Ok(new { Token = token });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            var profile = await _userService.GetProfileAsync(userId);
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UserProfileUpdateDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            await _userService.UpdateProfileAsync(userId, dto);
            return Ok("Profile updated successfully");
        }

        [HttpPost("profile-image")]
        [Authorize]
        public async Task<IActionResult> SetProfileImage(IFormFile file)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(folderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            await _userService.SetProfileImageAsync(userId, imageUrl);

            return Ok(new { Url = imageUrl, Message = "Profile image updated successfully" });
        }
    }
}
