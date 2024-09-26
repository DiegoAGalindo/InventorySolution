using InventoryAPIRead.Data;
using InventoryAPIRead.Repositories;
using InventorySharedLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests.Repositories
{
    public class InventoryMovementRepositoryTests
    {
        private DbContextOptions<InventoryContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetMovementsByProductIdAsync_ReturnsAllMovementsForProduct()
        {
            var options = GetDbContextOptions();
            using (var context = new InventoryContext(options))
            {
                var movement1 = new InventoryMovement(1, MovementType.In, 5);
                var movement2 = new InventoryMovement(1, MovementType.Out, 2);
                var movement3 = new InventoryMovement(2, MovementType.In, 10);

                context.InventoryMovements.Add(movement1);
                context.InventoryMovements.Add(movement2);
                context.InventoryMovements.Add(movement3);
                await context.SaveChangesAsync();
            }

            using (var context = new InventoryContext(options))
            {
                var repository = new InventoryMovementReadRepository(context);

                var movements = await repository.GetMovementsByProductIdAsync(1);

                Assert.Equal(2, movements.Count());
            }
        }
    }
}