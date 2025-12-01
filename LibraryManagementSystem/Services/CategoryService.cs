using LibraryManagementSystem.DTOs.CategoryDTOs;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Exceptions;

namespace LibraryManagementSystem.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto dto);
        Task DeleteAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category { Name = dto.Name };
            category = await _repo.AddAsync(category);

            if (category == null)
                throw new BadRequestException("Unable to create category");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found");

            category.Name = dto.Name;
            await _repo.UpdateAsync(category);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found");

            await _repo.DeleteAsync(category);
        }
    }
}
