using InventoryAPIRead.Controllers;
using InventoryAPIWrite.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class ProductsWriteControllerTests
    {
        [Fact]
        public async Task AddProduct_ThrowsException_WhenDatabaseFails()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .ThrowsAsync(new Exception("Database error"));

            var controller = new ProductsWriteController(mockProductRepo.Object);

            await Assert.ThrowsAsync<Exception>(async () =>
                await controller.PostProduct(new ProductWriteDTO
                { Name = "New Product", Price = 1000M, Stock = 5, CategoryId = 1 })
            );
        }

        [Fact]
        public async Task AddProduct_AddsProductSuccessfully()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(1);

            var controller = new ProductsWriteController(mockProductRepo.Object);

            var result = await controller.PostProduct(new ProductWriteDTO
            {
                Name = "Laptop",
                Description = "High-end laptop",
                Price = 1200.00M,
                Stock = 10,
                CategoryId = 1
            });

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Product>(okResult.Value);
            //Product value = okResult.Value;
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task PutProduct_UpdatesProductSuccessfully_ReturnsNoContent()
        {
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(repo => repo.UpdateProductAsync(It.IsAny<int>(), It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var controller = new ProductsWriteController(mockRepo.Object);

            var productDto = new ProductWriteDTO
            {
                Name = "Laptop",
                Description = "High-end laptop",
                Price = 1500.00M,
                Stock = 20,
                CategoryId = 1
            };

            var result = await controller.PutProduct(1, productDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutProduct_ThrowsException_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(repo => repo.UpdateProductAsync(It.IsAny<int>(), It.IsAny<Product>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new ProductsWriteController(mockRepo.Object);

            var productDto = new ProductWriteDTO
            {
                Name = "Laptop",
                Description = "High-end laptop",
                Price = 1500.00M,
                Stock = 20,
                CategoryId = 1
            };

            var result = await controller.PutProduct(1, productDto);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error: Unexpected error", statusCodeResult.Value);
        }

        [Fact]
        public async Task DeleteProduct_DeletesProductSuccessfully_ReturnsNoContent()
        {
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(repo => repo.DeleteProductAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var controller = new ProductsWriteController(mockRepo.Object);

            var result = await controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ThrowsException_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(repo => repo.DeleteProductAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new ProductsWriteController(mockRepo.Object);

            var result = await controller.DeleteProduct(1);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error: Unexpected error", statusCodeResult.Value);
        }
    }
}