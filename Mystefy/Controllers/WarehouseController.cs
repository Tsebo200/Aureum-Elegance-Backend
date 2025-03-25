using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public WarehouseController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            return await _context.Warehouses.ToListAsync();
        }

        // GET: api/Warehouse/{id}
        [HttpGet("{WarehouseID}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int WarehouseID)
        {
            var warehouse = await _context.Warehouses.FindAsync(WarehouseID);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // POST: api/Warehouse
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWarehouse), new { Warehouse= warehouse.WarehouseID }, warehouse);
        }

        // PUT: api/Warehouse/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.WarehouseID)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // DELETE: api/Warehouse/{id}
        [HttpDelete("{WarehouseID}")]
        public async Task<IActionResult> DeleteWarehouse(int WarehouseID)
        {
            var warehouse = await _context.Warehouses.FindAsync(WarehouseID);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(int WarehouseID)
        {
            return _context.Warehouses.Any(e => e.WarehouseID == WarehouseID);
        }
    }
}
