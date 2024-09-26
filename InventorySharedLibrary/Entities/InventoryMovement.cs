namespace InventorySharedLibrary.Entities
{
    public enum MovementType
    {
        In,
        Out
    }

    public class InventoryMovement
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public MovementType MovementType { get; private set; }
        public int Quantity { get; private set; }
        public DateTime MovementDate { get; private set; }

        public InventoryMovement()
        {
        }

        public InventoryMovement(int productId, MovementType movementType, int quantity)
        {
            ProductId = productId;
            MovementType = movementType;
            Quantity = quantity;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}