using InventorySharedLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPIRead.Data
{
    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<InventoryMovement>().ToTable("InventoryMovements");
        }
    }
}