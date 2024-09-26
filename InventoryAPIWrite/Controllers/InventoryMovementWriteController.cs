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
    public class InventoryMovementWriteController : ControllerBase
    {
        private readonly IInventoryMovementRepository _inventoryMovementRepository;

        public InventoryMovementWriteController(IInventoryMovementRepository inventoryMovementRepository)
        {
            _inventoryMovementRepository = inventoryMovementRepository;
        }

        /// <summary>
        /// Creates a new inventory movement.
        /// </summary>
        /// <param name="movementDto">The data for the inventory movement.</param>
        /// <returns>No content if the inventory movement is created successfully.</returns>
        /// <response code="204">Returns no content if the inventory movement is created successfully.</response>
        /// <response code="500">If there is an internal server error.</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Create a new inventory movement", Description = "Creates a new inventory movement, such as adding or removing stock from inventory.")]
        public async Task<ActionResult> PostInventoryMovement(InventoryMovementWriteDTO movementDto)
        {
            try
            {
                var movement = new InventoryMovement(movementDto.ProductId,
                    Enum.Parse<MovementType>(movementDto.MovementType), movementDto.Quantity);
                await _inventoryMovementRepository.AddInventoryMovementAsync(movement);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}