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
    public class FinishedProductController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public FinishedProductController(MystefyDbContext context)
        {
            _context = context;
        }

        // FINISHED PRODUCT ENDPOINTS

        // GET: api/Mystefy/finished-product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinishedProduct>>> GetFinishedProduct()
        {
            return await _context.FinishedProduct.ToListAsync();
        }

        // GET: api/Mystefy/finished-product
        [HttpGet("{id}")]
        public async Task<ActionResult<FinishedProduct>> GetFinishedProduct(int id)
        {
            var finishedProduct = await _context.FinishedProduct.FindAsync(id);
            if (finishedProduct == null)
            {
                return NotFound();
            }
            return finishedProduct;
        }

        // POST: api/Mystefy/finished-product
        [HttpPost]
        public async Task<ActionResult<FinishedProduct>> PostFinishedProduct(FinishedProduct finishedProduct)
        {
            _context.FinishedProduct.Add(finishedProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFinishedProduct), new { id = finishedProduct.ProductID }, finishedProduct);
        }

        // PUT: api/Mystefy/finished-product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinishedProduct(int id, FinishedProduct finishedProduct)
        {
            if (id != finishedProduct.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(finishedProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinishedProductExists(id))
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

        // DELETE: api/Mystefy/finished-product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinishedProduct(int id)
        {
            var finishedProduct = await _context.FinishedProduct.FindAsync(id);
            if (finishedProduct == null)
            {
                return NotFound();
            }

            _context.FinishedProduct.Remove(finishedProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper Method to Check If a Finished Product Exists
        private bool FinishedProductExists(int id)
        {
            return _context.FinishedProduct.Any(e => e.ProductID == id);
        }
    }
}
