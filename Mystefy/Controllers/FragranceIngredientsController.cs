using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
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
        public async Task<ActionResult<IEnumerable<FragranceIngredientsDTO>>> GetFragranceIngredients()
        {
            var fragranceIngredients = await _fragranceIngredientService.GetAllFragranceIngredients();

            var fragranceIngredientsList = fragranceIngredients.Select(fi => new FragranceIngredientsDTO
            {
                FragranceID = fi.FragranceID,
                IngredientsID = fi.IngredientsID,
                Amount = fi.Amount,
                Fragrances = fi.Fragrance == null ? null : new IncludeFragranceInFragranceIngredientsDTO
                {
                    Name = fi.Fragrance.Name,
                    Description = fi.Fragrance.Description,
                    Volume = fi.Fragrance.Volume,
                    Cost = fi.Fragrance.Cost,
                    ExpiryDate = fi.Fragrance.ExpiryDate
                },
                Ingredients = fi.Ingredients == null ? null : new IncludeIngredientInFragranceIngredientsDTO
                {
                    Id = fi.Ingredients.Id,
                    Name = fi.Ingredients.Name,
                    Type = fi.Ingredients.Type,
                    Cost = fi.Ingredients.Cost,
                    ExpiryDate = fi.Ingredients.ExpiryDate,
                    IsExpired = fi.Ingredients.IsExpired
                }
            });

            return Ok(fragranceIngredientsList);
        }

        [HttpGet("{fragranceId}/{ingredientId}")]
        public async Task<ActionResult<FragranceIngredientsDTO>> GetFragranceIngredient(int fragranceId, int ingredientId)
        {
            var fragranceIngredientIDs = await _fragranceIngredientService.GetFragranceIgredientsById(fragranceId, ingredientId);
            if (fragranceIngredientIDs == null) return NotFound();

            var FragranceIngredientDtoIDs = new FragranceIngredientsDTO
            {
                FragranceID = fragranceIngredientIDs.FragranceID,
                IngredientsID = fragranceIngredientIDs.IngredientsID,
                Amount = fragranceIngredientIDs.Amount,
                Fragrances = fragranceIngredientIDs.Fragrance == null ? null : new IncludeFragranceInFragranceIngredientsDTO
                {
                    Name = fragranceIngredientIDs.Fragrance.Name,
                    Description = fragranceIngredientIDs.Fragrance.Description,
                    Volume = fragranceIngredientIDs.Fragrance.Volume,
                    Cost = fragranceIngredientIDs.Fragrance.Cost,
                    ExpiryDate = fragranceIngredientIDs.Fragrance.ExpiryDate
                },
                Ingredients = fragranceIngredientIDs.Ingredients == null ? null : new IncludeIngredientInFragranceIngredientsDTO
                {
                    Id = fragranceIngredientIDs.Ingredients.Id,
                    Name = fragranceIngredientIDs.Ingredients.Name,
                    Type = fragranceIngredientIDs.Ingredients.Type,
                    Cost = fragranceIngredientIDs.Ingredients.Cost,
                    ExpiryDate = fragranceIngredientIDs.Ingredients.ExpiryDate,
                    IsExpired = fragranceIngredientIDs.Ingredients.IsExpired
                }
            };

            return Ok(FragranceIngredientDtoIDs);
        }
        
        [HttpPost]
        public async Task<ActionResult<FragranceIngredient>> PostFragranceIngredient(PostFragranceIngredientsDTO postFragranceIngredientDto)
        {
            var fragranceIngredientPost = new FragranceIngredient{
                FragranceID = postFragranceIngredientDto.FragranceID,
                IngredientsID = postFragranceIngredientDto.IngredientsID,
                Amount = postFragranceIngredientDto.Amount
            };
            var newFragranceIngredient= await _fragranceIngredientService.AddFragranceIngredient(fragranceIngredientPost);

            return CreatedAtAction(
                nameof(GetFragranceIngredient),
                new { fragranceId = newFragranceIngredient.FragranceID, ingredientId = newFragranceIngredient.IngredientsID },
                newFragranceIngredient
            );
        }

        
        [HttpPut("{fragranceId}/{ingredientId}")]
        public async Task<IActionResult> PutFragranceIngredient(int fragranceId, int ingredientId,  [FromBody] PostFragranceIngredientsDTO UpdateDto)
        {
            // Optional: You can validate that the route values match the body
            if (fragranceId != UpdateDto.FragranceID || ingredientId != UpdateDto.IngredientsID)
            {
                return BadRequest("FragranceID and IngredientsID in URL must match the body.");
            }

            var updatedIngredient = new FragranceIngredient
            {
                FragranceID = UpdateDto.FragranceID,
                IngredientsID = UpdateDto.IngredientsID,
                Amount = UpdateDto.Amount
            };

            var success = await _fragranceIngredientService.UpdateFragranceIngredient(fragranceId, ingredientId, updatedIngredient);
            
            if (!success)
                return NotFound();

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
