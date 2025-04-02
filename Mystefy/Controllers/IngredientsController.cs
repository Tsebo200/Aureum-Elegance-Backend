//The controller uses route attributes to map HTTP requests to appropriate actions.
//The constructor injects the IIngredientRepository service, which handles the actual logic for ingredients.
//Each method is an endpoint in the API that interacts with the ingredient data, either creating, retrieving, or modifying it based on the HTTP request type. 
using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")] // Specifies the route for the controller
    [ApiController] // Marks the class as an API controller, enabling automatic model validation and other features
    public class IngredientsController : ControllerBase
    {
        // Declaring a private read-only field to access the ingredient repository service
        private readonly IIngredientRepository _service;

        // Constructor to inject the IIngredientRepository dependency
        public IngredientsController(IIngredientRepository service)
        {
            _service = service;
        }

        // Endpoint to create a new ingredient
        [HttpPost("create")] // HTTP POST request for creating a new ingredient
        public async Task<ActionResult<Ingredients>> CreateIngredient([FromBody] Ingredients ingredient)
        {
            // Calls the service to create the ingredient and await the result
            Ingredients newIngredient = await _service.CreateIngredientAsync(ingredient);
            return Ok(newIngredient); // Returns the newly created ingredient with a 200 OK status
        }

        // Endpoint to get an ingredient with its associated recipes
        [HttpGet("{ingredientId}")] // HTTP GET request to fetch ingredient by ID
        public async Task<ActionResult<Ingredients>> GetIngredientWithRecipes(int ingredientId)
        {
            // Calls the service to retrieve the ingredient with its recipes
            Ingredients? ingredient = await _service.GetIngredientWithRecipes(ingredientId);
            if (ingredient == null)
            {
                // Returns a 404 Not Found status if ingredient doesn't exist
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient); // Returns the ingredient with its recipes
        }

        // Endpoint to add an ingredient to a recipe
        [HttpPost("addtorecipe")] // HTTP POST request to add ingredient to recipe
        public async Task<ActionResult> AddIngredientToRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            // Calls the service to associate the ingredient with a recipe
            await _service.AddIngredientToRecipe(ingredientId, recipeId);
            return Ok("Ingredient added to recipe"); // Confirms the addition with a success message
        }

        // Endpoint to remove an ingredient from a recipe
        [HttpPost("removefromrecipe")] // HTTP POST request to remove ingredient from recipe
        public async Task<ActionResult> RemoveIngredientFromRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            // Calls the service to remove the ingredient from the recipe
            await _service.RemoveIngredientFromRecipe(ingredientId, recipeId);
            return Ok("Ingredient removed from recipe"); // Confirms the removal with a success message
        }
    }
}



