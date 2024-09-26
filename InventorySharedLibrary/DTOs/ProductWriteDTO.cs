namespace InventorySharedLibrary.DTOs
{
    /// <summary>
    /// DTO for creating or updating a product.
    /// </summary>
    public class ProductWriteDTO
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A brief description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The available stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The ID of the category to which the product belongs.
        /// </summary>
        public int CategoryId { get; set; }
    }
}