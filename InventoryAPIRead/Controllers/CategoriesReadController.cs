using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryAPIRead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesReadController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesReadController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get all categories", Description = "Returns a list of all categories available.")]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var categoryDTOs = new List<CategoryReadDTO>();

            foreach (var category in categories)
            {
                categoryDTOs.Add(new CategoryReadDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            }

            return Ok(categoryDTOs);
        }

        /// <summary>
        /// Retrieves a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID.</returns>
        /// <response code="200">Returns the requested category.</response>
        /// <response code="404">If the category is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get category by ID", Description = "Returns a single category based on the provided ID.")]
        public async Task<ActionResult<CategoryReadDTO>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryReadDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return Ok(categoryDTO);
        }
    }
}