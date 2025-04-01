using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRequestController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public StockRequestController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/StockRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequest>>> GetStockRequest()
        {
            return await _context.StockRequests
                .Include(s => s.Ingredients)
                .Include(s => s.User)
                .Include(s => s.Warehouse)
                .ToListAsync();
        }

        // GET: api/StockRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequest>> GetStockRequest(int id)
        {
            var stockRequest = await _context.StockRequests
                .Include(s => s.Ingredients)
                .Include(s => s.User)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (stockRequest == null)
            {
                return NotFound();
            }

            return stockRequest;
        }

        // PUT: api/StockRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequest(int id, StockRequest stockRequest)
        {
            if (id != stockRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockRequestExists(id))
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

        // POST: api/StockRequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockRequest>> PostStockRequest(StockRequest stockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StockRequests.Add(stockRequest);
            await _context.SaveChangesAsync();

            // Load the related entities after saving
            await _context.Entry(stockRequest)
                .Reference(s => s.Ingredients)
                .LoadAsync();
            await _context.Entry(stockRequest)
                .Reference(s => s.User)
                .LoadAsync();
            await _context.Entry(stockRequest)
                .Reference(s => s.Warehouse)
                .LoadAsync();

            return CreatedAtAction("GetStockRequest", new { id = stockRequest.Id }, stockRequest);
        }

        // DELETE: api/StockRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequest(int id)
        {
            var stockRequest = await _context.StockRequests.FindAsync(id);
            if (stockRequest == null)
            {
                return NotFound();
            }

            _context.StockRequests.Remove(stockRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockRequestExists(int id)
        {
            return _context.StockRequests.Any(e => e.Id == id);
        }
    }
}
