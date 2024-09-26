using InventoryAPIRead.Data;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPIRead.Repositories
{
    public class InventoryMovementReadRepository : IInventoryMovementRepository
    {
        private readonly InventoryContext _context;

        public InventoryMovementReadRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(int productId)
        {
            try
            {
                return await _context.InventoryMovements
                    .Where(m => m.ProductId == productId)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task AddInventoryMovementAsync(InventoryMovement movement)
        {
            throw new System.NotImplementedException();
        }
    }
}