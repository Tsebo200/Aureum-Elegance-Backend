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
        public async Task<ActionResult<IngredientsDTO>> GetIngredientWithRecipes(int ingredientId)
        {
            Ingredients? ingredient = await _service.GetIngredientWithRecipesAsync(ingredientId);
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(MapToDTO(ingredient));
        }

        // POST: api/Ingredients/addtorecipe?ingredientId=1&recipeId=5
        [HttpPost("addtorecipe")]
        public async Task<ActionResult> AddIngredientToRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            await _service.AddIngredientToRecipeAsync(ingredientId, recipeId);
            return Ok("Ingredient added to recipe");
        }

        // POST: api/Ingredients/removefromrecipe?ingredientId=1&recipeId=5
        [HttpPost("removefromrecipe")]
        public async Task<ActionResult> RemoveIngredientFromRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            await _service.RemoveIngredientFromRecipeAsync(ingredientId, recipeId);
            return Ok("Ingredient removed from recipe");
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
