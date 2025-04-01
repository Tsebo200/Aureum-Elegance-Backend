using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragranceController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public FragranceController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/Fragrance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fragrance>>> GetFragrances()
        {
            return await _context.Fragrances.ToListAsync();
        }

        // GET: api/Fragrance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Fragrance>> GetFragrance(int id)
        {
            var fragrance = await _context.Fragrances.FindAsync(id);

            if (fragrance == null)
            {
                return NotFound();
            }

            return fragrance;
        }

        // POST: api/Fragrance
        [HttpPost]
       // POST: api/Fragrance
    [HttpPost]
    public async Task<ActionResult<Fragrance>> PostFragrance(Fragrance fragrance)
    {
        // Ensure ExpiryDate is in UTC
        fragrance.ExpiryDate = DateTime.SpecifyKind(fragrance.ExpiryDate, DateTimeKind.Utc);

        // Add fragrance to the context
        _context.Fragrances.Add(fragrance);
        await _context.SaveChangesAsync();

        // Return the newly created fragrance with the status code 201 (Created)
        return CreatedAtAction("GetFragrance", new { id = fragrance.Id }, fragrance);
    }


        // PUT: api/Fragrance/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFragrance(int id, Fragrance fragrance)
        {
            if (id != fragrance.Id)
            {
                return BadRequest();
            }

            _context.Entry(fragrance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FragranceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Fragrance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFragrance(int id)
        {
            var fragrance = await _context.Fragrances.FindAsync(id);
            if (fragrance == null)
            {
                return NotFound();
            }

            _context.Fragrances.Remove(fragrance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FragranceExists(int id)
        {
            return _context.Fragrances.Any(e => e.Id == id);
        }
    }
}
