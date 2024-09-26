using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Entities;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryAPIWrite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesWriteController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesWriteController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The category data to create a new category.</param>
        /// <returns>The ID of the newly created category.</returns>
        /// <response code="200">Returns the ID of the created category.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Create a new category", Description = "Creates a new category with the provided data and returns the category ID.")]
        public async Task<ActionResult> PostCategory(CategoryWriteDTO categoryDto)
        {
            try
            {
                var category = new Category(categoryDto.Name, categoryDto.Description);
                var categoryId = await _categoryRepository.AddCategoryAsync(category);

                return Ok(new { id = categoryId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The updated category data.</param>
        /// <returns>No content if the update is successful.</returns>
        /// <response code="204">Returns no content if the update is successful.</response>
        /// <response code="404">If the category with the specified ID is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update a category", Description = "Updates the specified category with the provided data.")]
        public async Task<IActionResult> PutCategory(int id, CategoryWriteDTO categoryDto)
        {
            try
            {
                var category = new Category(categoryDto.Name, categoryDto.Description);
                await _categoryRepository.UpdateCategoryAsync(id, category);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        /// <response code="204">Returns no content if the deletion is successful.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete a category", Description = "Deletes the category with the specified ID.")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryRepository.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}