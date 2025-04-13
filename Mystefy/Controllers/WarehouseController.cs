using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
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
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehouses();

            var warehouseDtos = warehouses.Select(w => new WarehouseDTO{
                Name = w.Name,
                location = w.Location
            }).ToList();

            return Ok(warehouseDtos);
        }
        [HttpGet("WithStockRequests")]
        public async Task<ActionResult<IEnumerable<WarehouseStockRequestDTO>>> GetAllWarehousesAndStockRequests()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAndStockRequests();
            var warehouseDtos = warehouses.Select(w => new WarehouseStockRequestDTO
            {
                Name = w.Name,
                location = w.Location,
                StockRequests = w.StockRequests != null ? new WStockRequestsDTO
                {
                    Id = w.StockRequests.First().Id,
                    AmountRequested = w.StockRequests.First().AmountRequested,
                    Status = w.StockRequests.First().Status,
                    RequestDate = w.StockRequests.First().RequestDate
                }
                : null
            }).ToList();

            return Ok(warehouseDtos);
        }

        [HttpGet("WithWarehouseStock")]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetAllWarehousesAndWarehouseStock()
{
    var warehouses = await _warehouseService.GetAllWarehousesAndWarehouseStock();

    var warehouseDtos = warehouses.Select(w => new WarehouseShowStock
    {
        Name = w.Name,
        location = w.Location,
        WarehouseStocks = w.WarehouseStocks.Any() 
            ? w.WarehouseStocks.Select(ws => new wWarehouseStockDTO
            {
                StockID = ws.StockID,
                Stock = ws.Stock,
                WarehouseID = ws.WarehouseID,
                FragranceID = ws.FragranceID,
                Fragrances = ws.Fragrance != null 
                    ? new WarehouseStockAddFragranceDTO
                    {
                        Id = ws.Fragrance.Id,
                        Name = ws.Fragrance.Name,
                        Description = ws.Fragrance.Description,
                        Cost = ws.Fragrance.Cost,
                        ExpiryDate = ws.Fragrance.ExpiryDate,
                        Volume = ws.Fragrance.Volume
                    } 
                    : null
            }).ToList() 
            : null
    }).ToList();

    return Ok(warehouseDtos);
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
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseDTO warehouseDto)
        {
            var warehouse = new Warehouse{
                Name = warehouseDto.Name,
                Location = warehouseDto.location
            };
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
