using InventorySharedLibrary.Entities;

namespace InventorySharedLibrary.Interfaces
{
    public interface IInventoryMovementRepository
    {
        Task<IEnumerable<InventoryMovement>> GetMovementsByProductIdAsync(int productId);

        Task AddInventoryMovementAsync(InventoryMovement movement);
    }
}