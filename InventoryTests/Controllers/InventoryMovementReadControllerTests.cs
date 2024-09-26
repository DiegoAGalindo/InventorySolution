using InventoryAPIRead.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class InventoryMovementReadControllerTests
    {
        [Fact]
        public async Task GetMovementsByProductId_ReturnsMovements_WhenMovementsExist()
        {
            var mockRepo = new Mock<IInventoryMovementRepository>();

            mockRepo.Setup(repo => repo.GetMovementsByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<InventoryMovement>
                {
                    new InventoryMovement(1, MovementType.In, 10),
                    new InventoryMovement(1, MovementType.Out, 5)
                });

            var controller = new InventoryMovementReadController(mockRepo.Object);

            var result = await controller.GetMovementsByProductId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movements = Assert.IsType<List<InventoryMovementReadDTO>>(okResult.Value);
            Assert.Equal(2, movements.Count);
            Assert.Equal(MovementType.In.ToString(), movements[0].MovementType);
            Assert.Equal(MovementType.Out.ToString(), movements[1].MovementType);
        }

        [Fact]
        public async Task GetMovementsByProductId_ReturnsEmptyList_WhenNoMovementsExist()
        {
            var mockRepo = new Mock<IInventoryMovementRepository>();

            mockRepo.Setup(repo => repo.GetMovementsByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<InventoryMovement>());

            var controller = new InventoryMovementReadController(mockRepo.Object);

            var result = await controller.GetMovementsByProductId(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movements = Assert.IsType<List<InventoryMovementReadDTO>>(okResult.Value);
            Assert.Empty(movements);
        }
    }
}