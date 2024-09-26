using InventorySharedLibrary.DTOs;
using InventorySharedLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPIRead.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementReadController : ControllerBase
    {
        private readonly IInventoryMovementRepository _inventoryMovementRepository;

        public InventoryMovementReadController(IInventoryMovementRepository inventoryMovementRepository)
        {
            _inventoryMovementRepository = inventoryMovementRepository;
        }

        /// <summary>
        /// Retrieves a specific inventory movement by ID.
        /// </summary>
        /// <param name="id">The ID of the inventory movement to retrieve.</param>
        /// <returns>The inventory movement with the specified ID.</returns>
        /// <response code="200">Returns the requested inventory movement.</response>
        /// <response code="404">If the inventory movement is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("product/{productId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InventoryMovementReadDTO>>> GetMovementsByProductId(int productId)
        {
            var movements = await _inventoryMovementRepository.GetMovementsByProductIdAsync(productId);
            var movementDTOs = new List<InventoryMovementReadDTO>();

            foreach (var movement in movements)
            {
                movementDTOs.Add(new InventoryMovementReadDTO
                {
                    Id = movement.Id,
                    ProductId = movement.ProductId,
                    MovementType = movement.MovementType.ToString(),
                    Quantity = movement.Quantity,
                    MovementDate = movement.MovementDate
                });
            }

            return Ok(movementDTOs);
        }
    }
}