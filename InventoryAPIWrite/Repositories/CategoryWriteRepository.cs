using Dapper;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryAPIWrite.Repositories
{
    public class CategoryWriteRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;

        public CategoryWriteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            return new SqlConnection(connectionString);
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            var query = @"
                INSERT INTO Categories (Name, Description)
                VALUES (@Name, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = CreateConnection())
            {
                connection.Open();
                return await connection.QuerySingleAsync<int>(query, new
                {
                    category.Name,
                    category.Description
                });
            }
        }

        public async Task UpdateCategoryAsync(int id, Category category)
        {
            var query = @"
                UPDATE Categories
                SET Name = @Name, Description = @Description
                WHERE Id = @Id";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new
                {
                    category.Name,
                    category.Description,
                    Id = id
                });
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var query = "DELETE FROM Categories WHERE Id = @Id";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}