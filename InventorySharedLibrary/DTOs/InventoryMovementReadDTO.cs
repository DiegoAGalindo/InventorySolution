namespace InventorySharedLibrary.DTOs
{
    public class InventoryMovementReadDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
    }
}