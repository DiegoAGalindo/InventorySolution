namespace InventorySharedLibrary.DTOs
{ /// <summary>
  /// DTO for reading product data. </summary>
    public class ProductReadDTO
    {  /// <summary>
       /// The unique ID of the product. </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The available stock of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The category id of the product.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The category name.
        /// </summary>
        public string CategoryName { get; set; }
    }
}