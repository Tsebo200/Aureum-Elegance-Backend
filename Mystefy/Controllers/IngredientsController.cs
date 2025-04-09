//The controller uses route attributes to map HTTP requests to appropriate actions.
//The constructor injects the IIngredientRepository service, which handles the actual logic for ingredients.
//Each method is an endpoint in the API that interacts with the ingredient data, either creating, retrieving, or modifying it based on the HTTP request type. 
using System.Linq;
using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Interfaces;
using Mystefy.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientRepository _service;

        public IngredientsController(IIngredientRepository service)
        {
            _service = service;
        }

        // POST: api/Ingredients/create
        [HttpPost("create")]
        public async Task<ActionResult<IngredientsDTO>> CreateIngredient([FromBody] Ingredients ingredient)
        {
            Ingredients newIngredient = await _service.CreateIngredientAsync(ingredient);
            return Ok(MapToDTO(newIngredient));
        }

        // GET: api/Ingredients/{ingredientId}
        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<IngredientsDTO>> GetIngredientWithBatch(int ingredientId)
        {
            Ingredients? ingredient = await _service.GetIngredientWithBatchAsync(ingredientId);
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(MapToDTO(ingredient));
        }

        // POST: api/Ingredients/addtobatch?ingredientId=1&batchId=5
        [HttpPost("addtobatch")]
        public async Task<ActionResult> AddIngredientToBatch([FromQuery] int ingredientId, [FromQuery] int batchId)
        {
            await _service.AddIngredientToBatchAsync(ingredientId, batchId);
            return Ok("Ingredient added to batch");
        }

        // POST: api/Ingredients/removefrombatch?ingredientId=1&batchId=5
        [HttpPost("removefrombatch")]
        public async Task<ActionResult> RemoveIngredientFromBatch([FromQuery] int ingredientId, [FromQuery] int batchId)
        {
            await _service.RemoveIngredientFromBatchAsync(ingredientId, batchId);
            return Ok("Ingredient removed from batch");
        }

        // Helper method to map the Ingredients model to the IngredientsDTO.
        private IngredientsDTO MapToDTO(Ingredients ingredient)
        {
            return new IngredientsDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Type = ingredient.Type,
                Cost = ingredient.Cost,
                ExpiryDate = ingredient.ExpiryDate,
                IsExpired = ingredient.IsExpired
            };
        }
    }
}

//Exposes HTTP endpoints (using GET, POST) to create, retrieve, and update ingredient data.
//Uses dependency injection to call methods from an IIngredientRepository service 
// (which handles the actual data operations).
//Maps the Ingredients model to an IngredientsDTO before sending data back to the client. 
// This abstraction helps control which data is exposed.