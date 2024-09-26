namespace InventorySharedLibrary.DTOs
{
    /// <summary>
    /// DTO for creating or updating an inventory movement.
    /// </summary>
    public class InventoryMovementWriteDTO
    {
        /// <summary>
        /// The ID of the product involved in the inventory movement.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The type of inventory movement. Valid values are "IN" for stock addition or "OUT" for
        /// stock removal.
        /// </summary>
        public string MovementType { get; set; }

        /// <summary>
        /// The quantity of the product involved in the movement.
        /// </summary>
        public int Quantity { get; set; }
    }
}