using InventoryAPIRead.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class ProductsReadControllerTests
    {
        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(new List<Product>
                {
                    new Product("Laptop", "High-end laptop", 1200.00M, 10, 1),
                    new Product("Smartphone", "Flagship phone", 800.00M, 15, 2)
                });

            var controller = new ProductsReadController(mockProductRepo.Object);

            var result = await controller.GetProducts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductReadDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithCorrectProduct()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Product("Laptop", "High-end laptop", 1200.00M, 10,
                    1));

            var controller = new ProductsReadController(mockProductRepo.Object);

            var result = await controller.GetProduct(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductReadDTO>(okResult.Value);
            Assert.Equal("Laptop", returnValue.Name);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var controller = new ProductsReadController(mockProductRepo.Object);

            var result = await controller.GetProduct(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}