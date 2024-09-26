using InventoryAPIRead.Data;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPIRead.Repositories
{
    public class CategoryReadRepository : ICategoryRepository
    {
        private readonly InventoryContext _context;

        public CategoryReadRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<int> AddCategoryAsync(Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateCategoryAsync(int id, Category category)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}