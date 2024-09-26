using InventoryAPIRead.Controllers;
using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryTests.Controllers
{
    public class CategoriesReadControllerTests
    {
        [Fact]
        public async Task GetCategories_ReturnsOkResult_WithListOfCategories()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(repo => repo.GetAllCategoriesAsync())
                .ReturnsAsync(new List<Category>
                {
                    new Category("Electronics", "All electronic items"),
                    new Category("Books", "All kinds of books")
                });

            var controller = new CategoriesReadController(mockCategoryRepo.Object);

            var result = await controller.GetCategories();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CategoryReadDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Category)null);

            var controller = new CategoriesReadController(mockCategoryRepo.Object);

            var result = await controller.GetCategory(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task? GetCategory_Return_Category_WhenCategoryExist()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(
                    new Category("Electronics", "All electronic items")
                );

            var controller = new CategoriesReadController(mockCategoryRepo.Object);

            var result = await controller.GetCategory(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CategoryReadDTO>(okResult.Value);

            Assert.Equal("Electronics", returnValue.Name);
        }

        [Fact]
        public async Task GetCategories_ReturnsEmptyList_WhenNoCategoriesExist()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            mockCategoryRepo.Setup(repo => repo.GetAllCategoriesAsync())
                .ReturnsAsync(new List<Category>());

            var controller = new CategoriesReadController(mockCategoryRepo.Object);

            var result = await controller.GetCategories();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<CategoryReadDTO>>>(result);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var categories = Assert.IsAssignableFrom<IEnumerable<CategoryReadDTO>>(okResult.Value);
            Assert.Empty(categories);
        }
    }
}