using InventoryAPIRead.Controllers;
using InventoryAPIWrite.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class InventoryMovementWriteControllerTests
    {
        [Fact]
        public async Task PostInventoryMovement_AddsMovementSuccessfully_ReturnsNoContent()
        {
            var mockRepo = new Mock<IInventoryMovementRepository>();

            mockRepo.Setup(repo => repo.AddInventoryMovementAsync(It.IsAny<InventoryMovement>()))
                .Returns(Task.CompletedTask);

            var controller = new InventoryMovementWriteController(mockRepo.Object);

            var movementDto = new InventoryMovementWriteDTO
            {
                ProductId = 1,
                MovementType = "In",
                Quantity = 10
            };

            var result = await controller.PostInventoryMovement(movementDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostInventoryMovement_ThrowsException_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<IInventoryMovementRepository>();

            mockRepo.Setup(repo => repo.AddInventoryMovementAsync(It.IsAny<InventoryMovement>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new InventoryMovementWriteController(mockRepo.Object);

            var movementDto = new InventoryMovementWriteDTO
            {
                ProductId = 1,
                MovementType = "In",
                Quantity = 10
            };

            var result = await controller.PostInventoryMovement(movementDto);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}