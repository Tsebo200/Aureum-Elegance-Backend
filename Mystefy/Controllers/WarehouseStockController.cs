using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseStockController : ControllerBase
    {
        private readonly IWarehouseStockService _warehouseStockService;

        public WarehouseStockController(IWarehouseStockService warehouseStockService)
        {
            _warehouseStockService = warehouseStockService;
        }

        // POST: api/WarehouseStock
        [HttpPost]
        public async Task<ActionResult<WarehouseStock>> PostWarehouseStock(WarehouseStock warehouseStock)
        {
            var MadeStock = await _warehouseStockService.AddStockAsync(warehouseStock);
            return CreatedAtAction(nameof(GetWarehouseStock), new { StockID = MadeStock.StockID }, MadeStock);

        }   
        // GET: api/WarehouseStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseStock>>> GetWarehouseStocks()
        {
            return Ok(await _warehouseStockService.GetAllStockAsync());
        }

        // GET: api/WarehouseStock/{StockID}
        [HttpGet("{StockID}")]
        public async Task<ActionResult<WarehouseStock>> GetWarehouseStock(int StockID)
        {
            var warehouseStock = await _warehouseStockService.GetStockByIdAsync(StockID);

            if (warehouseStock == null)
            {
                return NotFound();
            }

            return warehouseStock;
        }
          // DELETE: api/WarehouseStock/{id}
        [HttpDelete("{StockID}")]
        public async Task<IActionResult> DeleteWarehouseStock(int StockID)
        {
            var deleted = await _warehouseStockService.DeleteStockAsync(StockID);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        } 
        }
}
