namespace InventorySharedLibrary.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public int CategoryId { get; private set; }

        public Category Category { get; private set; }

        public Product()
        {
        }

        public Product(string name, string description, decimal price, int stock, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}