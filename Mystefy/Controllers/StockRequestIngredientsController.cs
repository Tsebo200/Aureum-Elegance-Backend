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
    public class StockRequestIngredientsController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public StockRequestIngredientsController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/StockRequestIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestIngredients>>> GetStockRequestIngredients()
        {
            return await _context.StockRequestIngredients.ToListAsync();
        }

        // GET: api/StockRequestIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestIngredients>> GetStockRequestIngredients(int id)
        {
            var stockRequestIngredients = await _context.StockRequestIngredients.FindAsync(id);

            if (stockRequestIngredients == null)
            {
                return NotFound();
            }

            return stockRequestIngredients;
        }

        // PUT: api/StockRequestIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequestIngredients(int id, StockRequestIngredients stockRequestIngredients)
        {
            if (id != stockRequestIngredients.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockRequestIngredients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockRequestIngredientsExists(id))
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

        // POST: api/StockRequestIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockRequestIngredients>> PostStockRequestIngredients(StockRequestIngredients stockRequestIngredients)
        {
            _context.StockRequestIngredients.Add(stockRequestIngredients);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockRequestIngredients", new { id = stockRequestIngredients.Id }, stockRequestIngredients);
        }

        // DELETE: api/StockRequestIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequestIngredients(int id)
        {
            var stockRequestIngredients = await _context.StockRequestIngredients.FindAsync(id);
            if (stockRequestIngredients == null)
            {
                return NotFound();
            }

            _context.StockRequestIngredients.Remove(stockRequestIngredients);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockRequestIngredientsExists(int id)
        {
            return _context.StockRequestIngredients.Any(e => e.Id == id);
        }
    }
}
