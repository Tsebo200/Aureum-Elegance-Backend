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
    public class StockRequestPackagingsController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public StockRequestPackagingsController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/StockRequestPackagings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestPackagings>>> GetStockRequestPackagings()
        {
            return await _context.StockRequestPackagings.ToListAsync();
        }

        // GET: api/StockRequestPackagings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestPackagings>> GetStockRequestPackagings(int id)
        {
            var stockRequestPackagings = await _context.StockRequestPackagings.FindAsync(id);

            if (stockRequestPackagings == null)
            {
                return NotFound();
            }

            return stockRequestPackagings;
        }

        // PUT: api/StockRequestPackagings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequestPackagings(int id, StockRequestPackagings stockRequestPackagings)
        {
            if (id != stockRequestPackagings.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockRequestPackagings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockRequestPackagingsExists(id))
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

        // POST: api/StockRequestPackagings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockRequestPackagings>> PostStockRequestPackagings(StockRequestPackagings stockRequestPackagings)
        {
            _context.StockRequestPackagings.Add(stockRequestPackagings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockRequestPackagings", new { id = stockRequestPackagings.Id }, stockRequestPackagings);
        }

        // DELETE: api/StockRequestPackagings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequestPackagings(int id)
        {
            var stockRequestPackagings = await _context.StockRequestPackagings.FindAsync(id);
            if (stockRequestPackagings == null)
            {
                return NotFound();
            }

            _context.StockRequestPackagings.Remove(stockRequestPackagings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockRequestPackagingsExists(int id)
        {
            return _context.StockRequestPackagings.Any(e => e.Id == id);
        }
    }
}
