namespace InventorySharedLibrary.DTOs
{ /// <summary>
  /// DTO for reading category data. </summary>
    public class CategoryReadDTO
    {
        /// <summary>
        /// The unique ID of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Description of the category.
        /// </summary>
        public string Description { get; set; }
    }
}