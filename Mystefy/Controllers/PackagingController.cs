using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class packagingController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public packagingController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/packaging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Packaging>>> GetPackaging()
        {
            return await _context.Packaging.ToListAsync();
        }

        // GET: api/packaging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Packaging>> GetPackaging(int id)
        {
            var packaging = await _context.Packaging.FindAsync(id);

            if (packaging == null)
            {
                return NotFound();
            }

            return packaging;
        }

        // PUT: api/packaging/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackaging(int id, Packaging packaging)
        {
            if (id != packaging.Id)
            {
                return BadRequest();
            }

            _context.Entry(packaging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackagingExists(id))
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

        // POST: api/packaging
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Packaging>> PostPackaging(Packaging packaging)
        {
            _context.Packaging.Add(packaging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackaging", new { id = packaging.Id }, packaging);
        }

        // DELETE: api/packaging/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var packaging = await _context.Packaging.FindAsync(id);
            if (packaging == null)
            {
                return NotFound();
            }

            _context.Packaging.Remove(packaging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackagingExists(int id)
        {
            return _context.Packaging.Any(e => e.Id == id);
        }
    }
}


