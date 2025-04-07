using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchIngredientsController : ControllerBase
    {
        private readonly IBatchIngredientsRepository _repository;

        public BatchIngredientsController(IBatchIngredientsRepository repository)
        {
            _repository = repository;
        }

        // POST: api/BatchIngredients
        [HttpPost]
        public async Task<ActionResult<BatchIngredientsDTO>> CreateBatchIngredient([FromBody] BatchIngredientsDTO batchIngredientDto)
        {
            var batchIngredient = new BatchIngredients
            {
                BatchID = batchIngredientDto.BatchID,
                IngredientsID = batchIngredientDto.IngredientsID,
                Quantity = batchIngredientDto.Quantity
            };

            var created = await _repository.CreateBatchIngredientAsync(batchIngredient);
            return CreatedAtAction(nameof(GetBatchIngredient),
                new { batchID = created.BatchID, ingredientsID = created.IngredientsID },
                MapToDTO(created));
        }

        // GET: api/BatchIngredients/{batchID}/{ingredientsID}
        [HttpGet("{batchID}/{ingredientsID}")]
        public async Task<ActionResult<BatchIngredientsDTO>> GetBatchIngredient(int batchID, int ingredientsID)
        {
            var batchIngredient = await _repository.GetBatchIngredientAsync(batchID, ingredientsID);
            if (batchIngredient == null)
            {
                return NotFound();
            }
            return Ok(MapToDTO(batchIngredient));
        }

        // PUT: api/BatchIngredients/{batchID}/{ingredientsID}
        [HttpPut("{batchID}/{ingredientsID}")]
        public async Task<ActionResult<BatchIngredientsDTO>> UpdateBatchIngredient(int batchID, int ingredientsID, [FromBody] BatchIngredientsDTO batchIngredientDto)
        {
            if (batchID != batchIngredientDto.BatchID || ingredientsID != batchIngredientDto.IngredientsID)
            {
                return BadRequest("ID mismatch");
            }

            var batchIngredient = new BatchIngredients
            {
                BatchID = batchIngredientDto.BatchID,
                IngredientsID = batchIngredientDto.IngredientsID,
                Quantity = batchIngredientDto.Quantity
            };

            var updated = await _repository.UpdateBatchIngredientAsync(batchIngredient);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(MapToDTO(updated));
        }

        // DELETE: api/BatchIngredients/{batchID}/{ingredientsID}
        [HttpDelete("{batchID}/{ingredientsID}")]
        public async Task<IActionResult> DeleteBatchIngredient(int batchID, int ingredientsID)
        {
            var deleted = await _repository.DeleteBatchIngredientAsync(batchID, ingredientsID);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Helper method to map model to DTO
        private BatchIngredientsDTO MapToDTO(BatchIngredients batchIngredient)
        {
            return new BatchIngredientsDTO
            {
                BatchID = batchIngredient.BatchID,
                IngredientsID = batchIngredient.IngredientsID,
                Quantity = batchIngredient.Quantity
            };
        }
    }
}

