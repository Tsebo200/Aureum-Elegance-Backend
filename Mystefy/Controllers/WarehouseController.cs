using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouse _warehouseService;

        public WarehouseController(IWarehouse warehouseService)
        {
            _warehouseService = warehouseService;
        }
        

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            return Ok(await _warehouseService.GetAllWarehouses());
        }

        // GET: api/Warehouse/{id}
        [HttpGet("{WarehouseID}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int WarehouseID)
        {
             var warehouse = await _warehouseService.GetWarehouseById(WarehouseID);

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
            var createdWarehouse = await _warehouseService.MakeWarehouse(warehouse);
            return CreatedAtAction(nameof(GetWarehouse), new { WarehouseID = createdWarehouse.WarehouseID }, createdWarehouse);

            
        }

        // PUT: api/Warehouse/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, Warehouse warehouse)
        {
            var updated = await _warehouseService.UpdateWarehouse(id, warehouse);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Warehouse/{id}
        [HttpDelete("{WarehouseID}")]
        public async Task<IActionResult> DeleteWarehouse(int WarehouseID)
        {
             var deleted = await _warehouseService.DeleteWarehouse(WarehouseID);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}
