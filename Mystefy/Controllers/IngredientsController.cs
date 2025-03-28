using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Interfaces;
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

        [HttpPost("create")]
        public async Task<ActionResult<Ingredients>> CreateIngredient([FromBody] Ingredients ingredient)
        {
            Ingredients newIngredient = await _service.CreateIngredientAsync(ingredient);
            return Ok(newIngredient);
        }

        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<Ingredients>> GetIngredientWithRecipes(int ingredientId)
        {
            Ingredients? ingredient = await _service.GetIngredientWithRecipes(ingredientId);
            if (ingredient == null)
            {
                return NotFound("Ingredient not found");
            }
            return Ok(ingredient);
        }

        [HttpPost("addtorecipe")]
        public async Task<ActionResult> AddIngredientToRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            await _service.AddIngredientToRecipe(ingredientId, recipeId);
            return Ok("Ingredient added to recipe");
        }

        [HttpPost("removefromrecipe")]
        public async Task<ActionResult> RemoveIngredientFromRecipe([FromQuery] int ingredientId, [FromQuery] int recipeId)
        {
            await _service.RemoveIngredientFromRecipe(ingredientId, recipeId);
            return Ok("Ingredient removed from recipe");
        }
    }
}


