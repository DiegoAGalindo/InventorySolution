using InventoryAPIRead.Controllers;
using InventoryAPIWrite.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class CategoriesWriteControllerTests
    {
        [Fact]
        public async Task PutCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(repo => repo.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>()))
                .ThrowsAsync(new KeyNotFoundException());

            var controller = new CategoriesWriteController(mockCategoryRepo.Object);

            var result = await controller.PutCategory(999,
                new CategoryWriteDTO { Name = "NonExistent", Description = "This category does not exist" });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutCategory_UpdatesCategorySuccessfully_ReturnsNoContent()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            mockCategoryRepo.Setup(repo => repo.UpdateCategoryAsync(It.IsAny<int>(), It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            var controller = new CategoriesWriteController(mockCategoryRepo.Object);

            var categoryDto = new CategoryWriteDTO
            {
                Name = "Updated Category",
                Description = "Updated Description"
            };

            var result = await controller.PutCategory(1, categoryDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PostCategory_ReturnsServerError_WhenExceptionOccurs()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(repo => repo.AddCategoryAsync(It.IsAny<Category>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new CategoriesWriteController(mockCategoryRepo.Object);

            var result = await controller.PostCategory(new CategoryWriteDTO
            { Name = "Invalid", Description = "This will throw exception" });

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PostCategory_AddsCategorySuccessfully_ReturnsOkWithCategoryId()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            mockCategoryRepo.Setup(repo => repo.AddCategoryAsync(It.IsAny<Category>()))
                .ReturnsAsync(1);

            var controller = new CategoriesWriteController(mockCategoryRepo.Object);

            var categoryDto = new CategoryWriteDTO
            {
                Name = "New Category",
                Description = "New Description"
            };

            var result = await controller.PostCategory(categoryDto);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var value = okResult.Value;

            Assert.Equal(1, (int)value.GetType().GetProperty("id").GetValue(value, null));
        }

        [Fact]
        public async Task DeleteCategoryAsync_DeletesCategorySuccessfully_ReturnsNoContent()
        {
            var mockRepo = new Mock<ICategoryRepository>();

            mockRepo.Setup(repo => repo.DeleteCategoryAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var controller = new CategoriesWriteController(mockRepo.Object);

            var result = await controller.DeleteCategory(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ThrowsException_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<ICategoryRepository>();

            mockRepo.Setup(repo => repo.DeleteCategoryAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unexpected error"));

            var controller = new CategoriesWriteController(mockRepo.Object);

            var result = await controller.DeleteCategory(1);

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error: Unexpected error", statusCodeResult.Value);
        }
    }
}