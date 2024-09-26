namespace InventorySharedLibrary.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category()
        {
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}