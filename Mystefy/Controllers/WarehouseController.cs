using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;
using System.Collections.Generic;
using System.Linq;
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

            var warehouseDtos = warehouses.Select(w => new WarehouseDTO
            {
                Name = w.Name,
                location = w.Location,
                AssignedManagerUserId = w.AssignedManagerUserId
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
            var warehouse = new Warehouse
            {
                Name = warehouseDto.Name,
                Location = warehouseDto.location,
                AssignedManagerUserId = warehouseDto.AssignedManagerUserId
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

        [HttpGet("WasteLossIngredients/{warehouseId}")]
        public async Task<ActionResult<WarehouseWasteLossRecordsForIngredientsDTO>> GetWasteLossIngredientsByWarehouseId(int warehouseId)
        {
            var warehouse = await _warehouseService.GetWasteLossIngredientsByWarehouseId(warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var dto = new WarehouseWasteLossRecordsForIngredientsDTO
            {
                Name = warehouse.Name,
                location = warehouse.Location,
                WasteLossRecordIngredients = warehouse.WasteLossRecordIngredients
                    .Where(w => w != null)
                    .Select(w => new WarehouseWasteLossRecordIngredientsDTO
                    {
                        Id = w.Id,
                        QuantityLoss = w.QuantityLoss,
                        Reason = w.Reason,
                        DateOfLoss = w.DateOfLoss,
                        User = w.User != null ? new WarehouseUserDTO
                        {
                            UserId = w.User.UserId,
                            Name = w.User.Name,
                            Role = w.User.Role.ToString()
                        } : null,
                        Ingredients = w.Ingredients != null ? new WarehouseWasteLossGetIngredientsDTO
                        {
                            Id = w.Ingredients.Id,
                            Name = w.Ingredients.Name,
                            Type = w.Ingredients.Type,
                            Cost = w.Ingredients.Cost,
                            IsExpired = w.Ingredients.IsExpired
                        } : null
                    }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet("WasteLossPackaging/{warehouseId}")]
        public async Task<ActionResult<WarehouseWasteLossRecordsForPackagingDTO>> GetWasteLossPackagingByWarehouseId(int warehouseId)
        {
            var warehouse = await _warehouseService.GetWasteLossPackagingByWarehouseId(warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var dto = new WarehouseWasteLossRecordsForPackagingDTO
            {
                Name = warehouse.Name,
                location = warehouse.Location,
                WasteLossRecordPackaging = warehouse.WasteLossRecordPackaging
                    .Where(w => w != null)
                    .Select(w => new WarehouseWasteLossRecordPackagingDTO
                    {
                        Id = w.Id,
                        QuantityLoss = w.QuantityLoss,
                        Reason = w.Reason,
                        DateOfLoss = w.DateOfLoss,
                        User = w.User != null ? new WarehouseUserDTO
                        {
                            UserId = w.User.UserId,
                            Name = w.User.Name,
                            Role = w.User.Role.ToString()
                        } : null,
                        Packaging = w.Packaging != null ? new WarehouseWasteLossGetPackagingDTO
                        {
                            Id = w.Packaging.Id,
                            Name = w.Packaging.Name,
                            Type = w.Packaging.Type,
                            Stock = w.Packaging.Stock
                        } : null
                    }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet("WasteLossFragrance/{warehouseId}")]
        public async Task<ActionResult<WarehouseWasteLossRecordsForFragrancesDTO>> GetWasteLossFragranceByWarehouseId(int warehouseId)
        {
            var warehouse = await _warehouseService.GetWasteLossFragranceByWarehouseId(warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var dto = new WarehouseWasteLossRecordsForFragrancesDTO
            {
                Name = warehouse.Name,
                location = warehouse.Location,
                WasteLossRecordFragrance = warehouse.WasteLossRecordFragrance
                    .Where(w => w != null)
                    .Select(w => new WarehouseWasteLossRecordFragranceDTO
                    {
                        Id = w.Id,
                        QuantityLoss = w.QuantityLoss,
                        Reason = w.Reason,
                        DateOfLoss = w.DateOfLoss,
                        User = w.User != null ? new WarehouseUserDTO
                        {
                            UserId = w.User.UserId,
                            Name = w.User.Name,
                            Role = w.User.Role.ToString()
                        } : null,
                        Fragrances = w.Fragrance != null ? new WarehouseWasteLossGetFragranceDTO
                        {
                            Id = w.Fragrance.Id,
                            Name = w.Fragrance.Name,
                            Description = w.Fragrance.Description,
                            Cost = w.Fragrance.Cost,
                            ExpiryDate = w.Fragrance.ExpiryDate,
                            Volume = w.Fragrance.Volume
                        } : null
                    }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet("WasteLossFinishedProduct/{warehouseId}")]
        public async Task<ActionResult<WarehouseWasteLossRecordsForBatchFinishedProductsDTO>> GetWasteLossFinishedProductByWarehouseId(int warehouseId)
        {
            var warehouse = await _warehouseService.GetWasteLossBatchFinishedProductsByWarehouseId(warehouseId);

            if (warehouse == null)
            {
                return NotFound();
            }

            var dto = new WarehouseWasteLossRecordsForBatchFinishedProductsDTO
            {
                Name = warehouse.Name,
                location = warehouse.Location,
                WasteLossRecordBatchFinishedProduct = warehouse.WasteLossRecordBatchFinishedProducts
                    .Where(w => w != null)
                    .Select(w => new WarehouseWasteLossRecordBatchFinishedProductDTO
                    {
                        Id = w.Id,
                        QuantityLoss = w.QuantityLoss,
                        Reason = w.Reason,
                        DateOfLoss = w.DateOfLoss,
                        User = w.User != null ? new WarehouseUserDTO
                        {
                            UserId = w.User.UserId,
                            Name = w.User.Name,
                            Role = w.User.Role.ToString()
                        } : null,
                        FinishedProduct = w.FinishedProduct != null ? new WarehouseWasteLossGetFinishedProductDTO
                        {
                            ProductID = w.FinishedProduct.ProductID,
                            FragranceID = w.FinishedProduct.FragranceID,
                            ProductName = w.FinishedProduct.ProductName,
                            Quantity = w.FinishedProduct.Quantity
                        } : null,
                        Batch = w.Batch != null ? new WarehouseWasteLossRecordGetBatchDTO
                        {
                            BatchID = w.Batch.BatchID,
                            ProductionDate = w.Batch.ProductionDate,
                            BatchSize = w.Batch.BatchSize
                        } : null
                    }).ToList()
            };

            return Ok(dto);
        }
    }
}
