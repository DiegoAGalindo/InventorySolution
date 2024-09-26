using Dapper;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryAPIWrite.Repositories
{
    public class InventoryMovementWriteRepository : IInventoryMovementRepository
    {
        private readonly IConfiguration _configuration;

        public InventoryMovementWriteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AddInventoryMovementAsync(InventoryMovement movement)
        {
            var query = @"
                INSERT INTO InventoryMovements (ProductId, MovementType, Quantity, MovementDate)
                VALUES (@ProductId, @MovementType, @Quantity, GETDATE());";

            using (var connection = CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new
                {
                    movement.ProductId,
                    movement.MovementType,
                    movement.Quantity
                });
            }
        }

        public Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}