using Microsoft.AspNetCore.Mvc;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragranceIngredientsController : ControllerBase
    {
        private readonly IFragranceIngredientService _fragranceIngredientService;

        public FragranceIngredientsController(IFragranceIngredientService fragranceIngredientService)
        {
            _fragranceIngredientService = fragranceIngredientService;
        }

        // GET: api/FragranceIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FragranceIngredient>>> GetFragranceIngredients()
        {
              return Ok(await _fragranceIngredientService.GetAllFragranceIngredients());
        }

        [HttpGet("{fragranceId}/{ingredientId}")]
        public async Task<ActionResult<FragranceIngredient>> GetFragranceIngredient(int fragranceId, int ingredientId)
        {
            var fragranceIngredient = await _fragranceIngredientService.GetFragranceIgredientsById(fragranceId, ingredientId);
           return fragranceIngredient == null? NotFound() : Ok(fragranceIngredient);
            
        }

        
        [HttpPost]
        public async Task<ActionResult<FragranceIngredient>> PostFragranceIngredient(FragranceIngredient fragranceIngredient)
        {
            var newFragranceIngredient= await _fragranceIngredientService.AddFragranceIngredient(fragranceIngredient);

            return CreatedAtAction(
                nameof(GetFragranceIngredient),
                new { fragranceId = newFragranceIngredient.FragranceID, ingredientId = newFragranceIngredient.IngredientsID },
                newFragranceIngredient
            );
        }

        
        [HttpPut("{fragranceId}/{ingredientId}")]
        public async Task<IActionResult> PutFragranceIngredient(int fragranceId, int ingredientId, FragranceIngredient fragranceIngredient)
        {
            var success = await _fragranceIngredientService.UpdateFragranceIngredient(fragranceId, ingredientId, fragranceIngredient);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{fragranceId}/{ingredientId}")]
        public async Task<IActionResult> DeleteFragranceIngredient(int fragranceId, int ingredientId)
        {
            var deleted = await _fragranceIngredientService.DeleteFragranceIngredient(fragranceId, ingredientId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
