using InventoryAPIRead.Data;
using InventoryAPIRead.Repositories;
using InventoryAPIWrite.Repositories;
using InventorySharedLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace InventoryTests.Repositories
{
    public class CategoryRepositoryTests
    {
        private DbContextOptions<InventoryContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using (var context = new InventoryContext(options))
            {
                context.Categories.Add(new Category("Electronics", "All electronic items"));
                context.Categories.Add(new Category("Books", "All kinds of books"));
                await context.SaveChangesAsync();
            }

            await using (var context = new InventoryContext(options))
            {
                var repository = new CategoryReadRepository(context);

                var categories = await repository.GetAllCategoriesAsync();

                Assert.Equal(2, categories.Count());
            }
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsCategory_WhenCategoryExists()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            await using (var context = new InventoryContext(options))
            {
                var category = new Category("Electronics", "All electronic items");
                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            await using (var context = new InventoryContext(options))
            {
                var repository = new CategoryReadRepository(context);

                var category = await repository.GetCategoryByIdAsync(1);

                Assert.NotNull(category);
                Assert.Equal("Electronics", category.Name);
            }
        }
    }
}