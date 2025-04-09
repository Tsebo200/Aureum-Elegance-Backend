//The controller uses route attributes to map HTTP requests to appropriate actions.
//The constructor injects the IIngredientRepository service, which handles the actual logic for ingredients.
//Each method is an endpoint in the API that interacts with the ingredient data, either creating, retrieving, or modifying it based on the HTTP request type. 
using System.Collections.Generic;
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

        // GET: api/Ingredients/name/{ingredientName}
        [HttpGet("name/{ingredientName}")]
        public async Task<ActionResult<IngredientsDTO>> GetIngredientByName(string ingredientName)
        {
            Ingredients? ingredient = await _service.GetIngredientByNameAsync(ingredientName);
            if (ingredient == null)
                return NotFound("Ingredient not found");

            return Ok(MapToDTO(ingredient));
        }

        // GET: api/Ingredients/type/{ingredientType}
        [HttpGet("type/{ingredientType}")]
        public async Task<ActionResult<IngredientsDTO>> GetIngredientByType(string ingredientType)
        {
            Ingredients? ingredient = await _service.GetIngredientByTypeAsync(ingredientType);
            if (ingredient == null)
                return NotFound("Ingredient not found");

            return Ok(MapToDTO(ingredient));
        }

        // GET: api/Ingredients/cost/{ingredientCost}
        [HttpGet("cost/{ingredientCost}")]
        public async Task<ActionResult<IngredientsDTO>> GetIngredientByCost(string ingredientCost)
        {
            Ingredients? ingredient = await _service.GetIngredientByCostAsync(ingredientCost);
            if (ingredient == null)
                return NotFound("Ingredient not found");

            return Ok(MapToDTO(ingredient));
        }

        // GET: api/Ingredients/expired/{isExpired}
        [HttpGet("expired/{isExpired}")]
        public async Task<ActionResult<IngredientsDTO>> GetIngredientByIsExpired(bool isExpired)
        {
            Ingredients? ingredient = await _service.GetIngredientByIsExpiredAsync(isExpired);
            if (ingredient == null)
                return NotFound("No matching ingredient found");

            return Ok(MapToDTO(ingredient));
        }

        // PUT: api/Ingredients/update
        [HttpPut("update")]
        public async Task<ActionResult<IngredientsDTO>> UpdateIngredient([FromBody] Ingredients ingredient)
        {
            Ingredients? updated = await _service.UpdateIngredientAsync(ingredient);
            if (updated == null)
                return NotFound("Ingredient not found to update");

            return Ok(MapToDTO(updated));
        }

        // DELETE: api/Ingredients/delete/{ingredientId}
        [HttpDelete("delete/{ingredientId}")]
        public async Task<ActionResult<IngredientsDTO>> DeleteIngredient(int ingredientId)
        {
            Ingredients? deleted = await _service.DeleteIngredientAsync(ingredientId);
            if (deleted == null)
                return NotFound("Ingredient not found to delete");

            return Ok(MapToDTO(deleted));
        }

        // GET: api/Ingredients/all
        [HttpGet("all")]
        public async Task<ActionResult<List<IngredientsDTO>>> GetAllIngredients()
        {
            // Convert the IEnumerable to List explicitly.
            var ingredients = (await _service.GetAllIngredientsAsync()).ToList();
            return Ok(ingredients.Select(MapToDTO).ToList());
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