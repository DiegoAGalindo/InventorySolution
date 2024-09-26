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
    public class ProductsWriteController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsWriteController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product details to create.</param>
        /// <returns>The newly created product with its ID.</returns>
        /// <response code="200">Returns the newly created product with its ID.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Create a new product", Description = "Creates a new product with the provided details and returns the product with its generated ID.")]
        public async Task<ActionResult> PostProduct(ProductWriteDTO productDto)
        {
            var product = new Product(productDto.Name, productDto.Description, productDto.Price, productDto.Stock,
                productDto.CategoryId);
            var productId = await _productRepository.AddProductAsync(product);
            product.SetId(productId);
            return Ok(product);
        }

        /// <summary>
        /// Updates an existing product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDto">The updated product details.</param>
        /// <returns>No content if the update is successful.</returns>
        /// <response code="204">No content if the update is successful.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update a product", Description = "Updates the product with the specified ID using the provided details.")]
        public async Task<IActionResult> PutProduct(int id, ProductWriteDTO productDto)
        {
            try
            {
                var product = new Product(productDto.Name, productDto.Description, productDto.Price, productDto.Stock,
                    productDto.CategoryId);
                await _productRepository.UpdateProductAsync(id, product);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        /// <response code="204">No content if the deletion is successful.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete a product", Description = "Deletes the product with the specified ID.")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}