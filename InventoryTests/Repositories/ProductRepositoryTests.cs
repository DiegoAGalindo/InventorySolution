using InventoryAPIRead.Data;
using InventoryAPIRead.Repositories;
using InventorySharedLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests.Repositories
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<InventoryContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new InventoryContext(options))
            {
                context.Products.Add(new Product("Laptop", "High-end laptop", 1200.00M, 10, 1));
                context.Products.Add(new Product("Smartphone", "Flagship phone", 800.00M, 15, 2));
                await context.SaveChangesAsync();

                var count = context.Products.Count();
                Assert.Equal(2, count);
            }

            using (var context = new InventoryContext(options))
            {
                var repository = new ProductReadRepository(context);

                var products = await repository.GetAllProductsAsync();

                Assert.Equal(2, products.Count());
            }
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsCorrectProduct()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new InventoryContext(options))
            {
                var product = new Product("Laptop", "High-end laptop", 1200.00M, 10, 1);
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }

            using (var context = new InventoryContext(options))
            {
                var repository = new ProductReadRepository(context);

                var product = await repository.GetProductByIdAsync(1);

                Assert.NotNull(product);
                Assert.Equal("Laptop", product.Name);
            }
        }
    }
}