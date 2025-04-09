//The controller handles warehouse ingredient data with methods that allow the creation, retrieval, updating, and deletion of warehouse ingredients.
//The constructor injects the IWarehouseIngredients service for performing the actual operations on the database.
//Each HTTP method corresponds to a specific operation (create, read, update, delete) and uses appropriate HTTP status codes (200 OK, 201 Created, 404 Not Found, 400 Bad Request, 204 No Content).
using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")] // Specifies the route for the controller, where [controller] is a placeholder for the controller name
    [ApiController] // Marks the class as an API controller, enabling automatic model validation and other features
    public class WarehouseIngredientsController : ControllerBase
    {
        // Declaring a private read-only field to access the warehouse ingredient repository service
        private readonly IWarehouseIngredients _service;

        // Constructor to inject the IWarehouseIngredients dependency
        public WarehouseIngredientsController(IWarehouseIngredients service)
        {
            _service = service;
        }

        // Endpoint to create a new warehouse ingredient
        [HttpPost("create")] // HTTP POST request for creating a new ingredient
        public async Task<ActionResult<WarehouseIngredients>> CreateIngredient([FromBody] WarehouseIngredients ingredient)
        {
            // Calls the service to create the ingredient and await the result
            WarehouseIngredients newIngredient = await _service.CreateIngredientAsync(ingredient);
            return Ok(newIngredient); // Returns the newly created ingredient with a 200 OK status
        }

        // Endpoint to get a warehouse ingredient by its ID with details
        [HttpGet("{ingredientId}")] // HTTP GET request to fetch ingredient by ID
        public async Task<ActionResult<WarehouseIngredients>> GetIngredientWithDetails(int ingredientId)
        {
            // Calls the service to retrieve the ingredient with details
            WarehouseIngredients? ingredient = await _service.GetIngredientWithDetailsAsync(ingredientId);
            if (ingredient == null)
            {
                // Returns a 404 Not Found status if the ingredient doesn't exist
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient); // Returns the ingredient with its details
        }

        // Endpoint to get a warehouse ingredient by its name
        [HttpGet("name/{ingredientName}")] // HTTP GET request to fetch ingredient by name
        public async Task<ActionResult<WarehouseIngredients>> GetIngredientByName(string ingredientName)
        {
            // Calls the service to retrieve the ingredient by its name
            WarehouseIngredients? ingredient = await _service.GetIngredientByNameAsync(ingredientName);
            if (ingredient == null)
            {
                // Returns a 404 Not Found status if the ingredient doesn't exist
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient); // Returns the ingredient by its name
        }

        // Endpoint to update an existing warehouse ingredient
        [HttpPut("{ingredientId}")] // HTTP PUT request to update an ingredient by ID
        public async Task<ActionResult<WarehouseIngredients>> UpdateIngredient(int ingredientId, [FromBody] WarehouseIngredients ingredient)
        {
            if (ingredientId != ingredient.IngredientId)
            {
                // Returns a 400 Bad Request status if the ID doesn't match
                return BadRequest("ID mismatch");
            }
            // Calls the service to update the ingredient
            WarehouseIngredients? updatedIngredient = await _service.UpdateIngredientAsync(ingredient);
            if (updatedIngredient == null)
            {
                // Returns a 404 Not Found status if the ingredient doesn't exist
                return NotFound("Ingredient not found");
            }
            return Ok(updatedIngredient); // Returns the updated ingredient
        }

        // Endpoint to delete a warehouse ingredient
        [HttpDelete("{ingredientId}")] // HTTP DELETE request to remove an ingredient by ID
        public async Task<ActionResult> DeleteIngredient(int ingredientId)
        {
            // Calls the service to delete the ingredient
            WarehouseIngredients? deletedIngredient = await _service.DeleteIngredientAsync(ingredientId);
            if (deletedIngredient == null)
            {
                // Returns a 404 Not Found status if the ingredient doesn't exist
                return NotFound("Ingredient not found");
            }
            return NoContent(); // Returns a 204 No Content status to indicate the deletion was successful
        }
    }
}
