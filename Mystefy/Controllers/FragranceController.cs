using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragranceController : ControllerBase
    {
        private readonly IFragranceService _fragranceService;

        public FragranceController(IFragranceService fragranceService)
        {
            _fragranceService = fragranceService;
        }
        // GET: api/Fragrance
         [HttpGet]
        public async Task<ActionResult<IEnumerable<FragranceDTO>>> GetFragrances()
        {
            var fragrances = await _fragranceService.GetAllFragrances();

            var fragranceDtos = fragrances.Select(f => new FragranceDTO
            {
                Id = f.Id,
                Name = f.Name,
                Description = f.Description,
                Cost = f.Cost,
                ExpiryDate = f.ExpiryDate,
                Volume = f.Volume,
                FragranceIngredients = f.FragranceIngredients?.Select(fi => new FragranceIngredientInFragranceDTO
                {
                    IngredientsID = fi.IngredientsID,
                    Amount = fi.Amount,
                    Ingredients = fi.Ingredients != null ? new List<IngredientsInFragranceIngredientsDTO>
                    {
                        new IngredientsInFragranceIngredientsDTO
                        {
                            Name = fi.Ingredients.Name,
                            Type = fi.Ingredients.Type,
                            Cost = fi.Ingredients.Cost,
                            ExpiryDate = fi.Ingredients.ExpiryDate
                        }
                    } : null
                }).ToList()
            }).ToList();

            return Ok(fragranceDtos);
        }


        // GET: api/Fragrance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Fragrance>> GetFragrance(int id)
        {
           var fragrance = await _fragranceService.GetFragranceById(id);
           return fragrance == null? NotFound() : Ok(fragrance);
        }

       // POST: api/Fragrance
     [HttpPost]
        public async Task<IActionResult> AddFragrance([FromBody] PostFragranceDTO fragranceDto)
        {
            if (fragranceDto == null)
            {
                return BadRequest("Fragrance data is required.");
            }

            // Map the DTO to the Fragrance entity
            var fragrance = new Fragrance
            {
                Name = fragranceDto.Name,
                Description = fragranceDto.Description,
                ExpiryDate = DateTime.SpecifyKind(fragranceDto.ExpiryDate, DateTimeKind.Utc),
                Cost = fragranceDto.Cost,
                Volume = fragranceDto.Volume
            };
        // Ensure ExpiryDate is in UTC
        var newFragrance = await _fragranceService.AddFragrance(fragrance);
        return CreatedAtAction("GetFragrance", new { id = newFragrance.Id }, newFragrance);
    }


        // PUT: api/Fragrance/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFragrance(int id, Fragrance fragrance)
        {
              var updated = await _fragranceService.UpdateFragrance(id, fragrance);
            
            if(!updated){
                return NotFound();

            };
            return NoContent();
        }

        // DELETE: api/Fragrance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFragrance(int id)
        {
             var deleted = await _fragranceService.DeleteFragrance(id);
            if(!deleted) 
            {
                return NotFound();
            };

            return NoContent();
        }

        
    }
}
