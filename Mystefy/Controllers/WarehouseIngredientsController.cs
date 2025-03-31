using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseIngredientsController : ControllerBase
    {
        private readonly IWarehouseIngredients _service;

        public WarehouseIngredientsController(IWarehouseIngredients service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<WarehouseIngredients>> CreateIngredient([FromBody] WarehouseIngredients ingredient)
        {
            WarehouseIngredients newIngredient = await _service.CreateIngredientAsync(ingredient);
            return Ok(newIngredient);
        }

        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<WarehouseIngredients>> GetIngredientWithDetails(int ingredientId)
        {
            WarehouseIngredients? ingredient = await _service.GetIngredientWithDetailsAsync(ingredientId);
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient);
        }

        [HttpGet("name/{ingredientName}")]
        public async Task<ActionResult<WarehouseIngredients>> GetIngredientByName(string ingredientName)
        {
            WarehouseIngredients? ingredient = await _service.GetIngredientByNameAsync(ingredientName);
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient);
        }

        [HttpPut("{ingredientId}")]
        public async Task<ActionResult<WarehouseIngredients>> UpdateIngredient(int ingredientId, [FromBody] WarehouseIngredients ingredient)
        {
            if (ingredientId != ingredient.IngredientId)
            {
                return BadRequest("ID mismatch");
            }
            WarehouseIngredients? updatedIngredient = await _service.UpdateIngredientAsync(ingredient);
            if (updatedIngredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(updatedIngredient);
        }

        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult> DeleteIngredient(int ingredientId)
        {
            WarehouseIngredients? deletedIngredient = await _service.DeleteIngredientAsync(ingredientId);
            if (deletedIngredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return NoContent();
        }
    }
}
