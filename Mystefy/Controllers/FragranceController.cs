using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
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
        public async Task<ActionResult<IEnumerable<Fragrance>>> GetFragrances()
        {
            return Ok(await _fragranceService.GetAllFragrances());
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
    public async Task<ActionResult<Fragrance>> PostFragrance(Fragrance fragrance)
    {
        // Ensure ExpiryDate is in UTC
        var newFragrance = await _fragranceService.AddFragrance(fragrance);
        // Return the newly created fragrance with the status code 201 (Created)
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
