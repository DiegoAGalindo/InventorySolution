using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryAPIRead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsReadController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsReadController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get all products", Description = "Returns a list of all products available in the inventory.")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDTOs = new List<ProductReadDTO>();

            foreach (var product in products)
            {
                productDTOs.Add(new ProductReadDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId
                });
            }

            return Ok(productDTOs);
        }

        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get product by ID", Description = "Returns a specific product based on the provided ID.")]
        public async Task<ActionResult<ProductReadDTO>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };

            return Ok(productDTO);
        }
    }
}