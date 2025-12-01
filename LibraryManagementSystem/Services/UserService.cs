using LibraryManagementSystem.DTOs.UserDTOs;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Exceptions;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto, string role);
        Task<string> LoginAsync(LoginDto dto);
        Task<UserProfileDto> GetProfileAsync(string userId);
        Task UpdateProfileAsync(string userId, UserProfileUpdateDto dto);
        Task SetProfileImageAsync(string userId, string imageUrl);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task RegisterAsync(RegisterUserDto dto, string role)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email
            };

            await _userRepository.CreateAsync(user, dto.Password);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedException("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName ?? "")
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserProfileDto> GetProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User not found");

            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName ?? "",
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl
            };
        }

        public async Task UpdateProfileAsync(string userId, UserProfileUpdateDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User not found");

            if (!string.IsNullOrEmpty(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrEmpty(dto.Email))
            {
                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            await _userRepository.UpdateAsync(user);
        }

        public async Task SetProfileImageAsync(string userId, string imageUrl)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User not found");

            user.ProfileImageUrl = imageUrl;
            await _userRepository.UpdateAsync(user);
        }
    }
}
