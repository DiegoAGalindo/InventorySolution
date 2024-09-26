using Dapper;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryAPIWrite.Repositories
{
    public class ProductWriteRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductWriteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var query = @"
                INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
                VALUES (@Name, @Description, @Price, @Stock, @CategoryId);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = CreateConnection())
            {
                connection.Open();
                return await connection.QuerySingleAsync<int>(query, new
                {
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Stock,
                    product.CategoryId
                });
            }
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var query = @"
                UPDATE Products
                SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock, CategoryId = @CategoryId
                WHERE Id = @Id";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new
                {
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Stock,
                    product.CategoryId,
                    Id = id
                });
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}