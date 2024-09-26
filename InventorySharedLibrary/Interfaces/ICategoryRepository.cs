using InventorySharedLibrary.Entities;

namespace InventorySharedLibrary.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        Task<int> AddCategoryAsync(Category category);

        Task UpdateCategoryAsync(int id, Category category);

        Task DeleteCategoryAsync(int id);
    }
}