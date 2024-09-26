using InventoryAPIRead.Data;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPIRead.Repositories
{
    public class ProductReadRepository : IProductRepository
    {
        private readonly InventoryContext _context;

        public ProductReadRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<int> AddProductAsync(Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateProductAsync(int id, Product product)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}