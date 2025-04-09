using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
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
        public async Task<ActionResult<WarehouseStock>> PostWarehouseStock(CreateWarehouseStockDTO warehouseStockDTO)
        {
              var warehouseStock = new WarehouseStock
              {
                Stock = warehouseStockDTO.Stock,
                WarehouseID = warehouseStockDTO.WarehouseID,
                FragranceID = warehouseStockDTO.FragranceID
                };
            var MadeStock = await _warehouseStockService.AddStockAsync(warehouseStock);
            return CreatedAtAction(nameof(GetWarehouseStock), new { StockID = MadeStock.StockID }, MadeStock);

        }   
        // GET: api/WarehouseStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseStockDTO>>> GetWarehouseStocks()
        {
            var warehouseStock = await _warehouseStockService.GetAllStockAsync();

            var warehouseStockDtos = warehouseStock.Select(ws => new WarehouseStockDTO{
                StockID = ws.StockID,
                Stock = ws.Stock,
                WarehouseID = ws.WarehouseID,
                Warehouses = ws.Warehouse != null ? new CheckWarehouseStockWarehouseDTO
                {
                    Id = ws.Warehouse.WarehouseID,
                    Name = ws.Warehouse.Name,
                    Location = ws.Warehouse.Location,

                }: null,
                FragranceID = ws.FragranceID,
                 Fragrances = ws.Fragrance != null ? new WarehouseStockFragranceDTO
                {
                    Id = ws.Fragrance.Id,
                        Name = ws.Fragrance.Name,
                        Description = ws.Fragrance.Description,
                        Cost = ws.Fragrance.Cost,
                        ExpiryDate = ws.Fragrance.ExpiryDate,
                        Volume = ws.Fragrance.Volume

                }: null
            }).ToList();

            return Ok(warehouseStockDtos);
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

             var warehouseStockDto = new WarehouseStockDTO
    {
        StockID = warehouseStock.StockID,
        Stock = warehouseStock.Stock,
        WarehouseID = warehouseStock.WarehouseID,
        Warehouses = warehouseStock.Warehouse != null ? new CheckWarehouseStockWarehouseDTO
        {
            Id = warehouseStock.Warehouse.WarehouseID,
            Name = warehouseStock.Warehouse.Name,
            Location = warehouseStock.Warehouse.Location,
        } : null,
        FragranceID = warehouseStock.FragranceID,
        Fragrances = warehouseStock.Fragrance != null ? new WarehouseStockFragranceDTO
        {
            Id = warehouseStock.Fragrance.Id,
            Name = warehouseStock.Fragrance.Name,
            Description = warehouseStock.Fragrance.Description,
            Cost = warehouseStock.Fragrance.Cost,
            ExpiryDate = warehouseStock.Fragrance.ExpiryDate,
            Volume = warehouseStock.Fragrance.Volume
        } : null
    };

    return Ok(warehouseStockDto);
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
