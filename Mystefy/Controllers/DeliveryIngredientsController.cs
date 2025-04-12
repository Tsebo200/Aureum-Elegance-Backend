using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryIngredientsController : ControllerBase
    {
        private readonly IDeliveryIngredientsRepository _deliveryIngredientsService;

        public DeliveryIngredientsController(IDeliveryIngredientsRepository deliveryIngredientsService)
        {
            _deliveryIngredientsService = deliveryIngredientsService;
        }

        // POST: api/DeliveryIngredients
        [HttpPost]
        public async Task<ActionResult<DeliveryIngredientsDTO>> CreateDeliveryIngredient([FromBody] DeliveryIngredients deliveryIngredient)
        {
            var createdRecord = await _deliveryIngredientsService.CreateDeliveryIngredientsAsync(deliveryIngredient);
            return CreatedAtAction(nameof(GetDeliveryIngredient), new
            {
                deliveryIngredientID = createdRecord.DeliveryIngredientID,
                ingredientID = createdRecord.IngredientID
            }, MapToDTO(createdRecord));
        }

        // GET: api/DeliveryIngredients/{deliveryIngredientID}/{ingredientID}
        [HttpGet("{deliveryIngredientID}/{ingredientID}")]
        public async Task<ActionResult<DeliveryIngredientsDTO>> GetDeliveryIngredient(int deliveryIngredientID, int ingredientID)
        {
            var record = await _deliveryIngredientsService.GetDeliveryIngredientAsync(deliveryIngredientID, ingredientID);
            if (record == null)
                return NotFound("Delivery Ingredient record not found");
            return Ok(MapToDTO(record));
        }

        // GET: api/DeliveryIngredients (List All)
        [HttpGet("all")]
        public async Task<ActionResult<List<DeliveryIngredientsDTO>>> GetAllDeliveryIngredients()
        {
            var records = await _deliveryIngredientsService.GetAllDeliveryIngredientsAsync();
            var dtos = records.Select(r => MapToDTO(r)).ToList();
            return Ok(dtos);
        }

        // PUT: api/DeliveryIngredients/{deliveryIngredientID}/{ingredientID}
        [HttpPut("{deliveryIngredientID}/{ingredientID}")]
        public async Task<ActionResult<DeliveryIngredientsDTO>> UpdateDeliveryIngredient(
            int deliveryIngredientID, int ingredientID, [FromBody] DeliveryIngredients deliveryIngredient)
        {
            // Ensure the keys match
            if (deliveryIngredient.DeliveryIngredientID != deliveryIngredientID || deliveryIngredient.IngredientID != ingredientID)
                return BadRequest("ID mismatch");

            var updatedRecord = await _deliveryIngredientsService.UpdateDeliveryIngredientsAsync(deliveryIngredient);
            if (updatedRecord == null)
                return NotFound("Delivery Ingredient record not found");

            return Ok(MapToDTO(updatedRecord));
        }

        // DELETE: api/DeliveryIngredients/{deliveryIngredientID}/{ingredientID}
        [HttpDelete("{deliveryIngredientID}/{ingredientID}")]
        public async Task<IActionResult> DeleteDeliveryIngredient(int deliveryIngredientID, int ingredientID)
        {
            bool success = await _deliveryIngredientsService.DeleteDeliveryIngredientsAsync(deliveryIngredientID, ingredientID);
            if (!success)
                return NotFound("Delivery Ingredient record not found");

            return NoContent();
        }

        // Helper method: Maps a DeliveryIngredients entity to its DTO.
        private DeliveryIngredientsDTO MapToDTO(DeliveryIngredients di)
        {
            return new DeliveryIngredientsDTO
            {
                DeliveryIngredientID = di.DeliveryIngredientID,
                IngredientID = di.IngredientID,
                QuantityDelivered = di.QuantityDelivered,
                DateOrdered = di.DateOrdered,
                DeliveryCost = di.DeliveryIngredientCost
            };
        }
    }
}

