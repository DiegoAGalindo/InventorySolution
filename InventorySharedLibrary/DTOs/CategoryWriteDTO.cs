namespace InventorySharedLibrary.DTOs
{
    /// <summary>
    /// DTO used for creating or updating a category.
    /// </summary>
    public class CategoryWriteDTO
    {
        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A brief description of the category.
        /// </summary>
        public string Description { get; set; }
    }
}