using InventorySharedLibrary.Entities;

namespace InventorySharedLibrary.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<int> AddProductAsync(Product product);

        Task UpdateProductAsync(int id, Product product);

        Task DeleteProductAsync(int id);
    }
}