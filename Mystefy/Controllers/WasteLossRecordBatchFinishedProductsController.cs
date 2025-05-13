using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteLossRecordBatchFinishedProductsController : ControllerBase
    {
        private readonly IWasteLossRecordBatchFinishedProductsRepository _batchFinishedProductsService;

        public WasteLossRecordBatchFinishedProductsController(IWasteLossRecordBatchFinishedProductsRepository batchFinishedProductsService)
        {
            _batchFinishedProductsService = batchFinishedProductsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WasteLossRecordBatchFinishedProductsDTO>>> GetAllWasteLossRecords()
        {
            var records = await _batchFinishedProductsService.GetAllWasteLossRecordsAsync();

            var dtoList = records.Select(record => new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordBatchFinishedProductsUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                FinishedProduct = record.FinishedProduct != null ? new WasteLossRecordBatchFinishedProductsFinishedProductDTO
                {
                    ProductID = record.FinishedProduct.ProductID,
                    FragranceID = record.FinishedProduct.FragranceID,
  
                    Quantity = record.FinishedProduct.Quantity
                } : null,
                Batch = record.Batch != null ? new WasteLossRecordBatchFinishedProductsBatchDTO
                {
                    BatchID = record.Batch.BatchID,
                    ProductionDate = record.Batch.ProductionDate,
                    BatchSize = record.Batch.BatchSize
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordBatchFinishedProductsWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WasteLossRecordBatchFinishedProductsDTO>> GetWasteLossRecordById(int id)
        {
            var record = await _batchFinishedProductsService.GetWasteLossRecordByIdAsync(id);

            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordBatchFinishedProductsUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                FinishedProduct = record.FinishedProduct != null ? new WasteLossRecordBatchFinishedProductsFinishedProductDTO
                {
                    ProductID = record.FinishedProduct.ProductID,
                    FragranceID = record.FinishedProduct.FragranceID,
                  
                    Quantity = record.FinishedProduct.Quantity
                } : null,
                Batch = record.Batch != null ? new WasteLossRecordBatchFinishedProductsBatchDTO
                {
                    BatchID = record.Batch.BatchID,
                    ProductionDate = record.Batch.ProductionDate,
                    BatchSize = record.Batch.BatchSize
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordBatchFinishedProductsWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WasteLossRecordBatchFinishedProductsDTO>> CreateWasteLossRecord(Mystefy.Models.WasteLossRecordBatchFinishedProducts record)
        {
            var createdRecord = await _batchFinishedProductsService.CreateWasteLossRecordAsync(record);

            var dto = new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = createdRecord.Id,
                QuantityLoss = createdRecord.QuantityLoss,
                Reason = createdRecord.Reason,
                DateOfLoss = createdRecord.DateOfLoss
                // Related objects will remain null unless eager loaded
            };

            return CreatedAtAction(nameof(GetWasteLossRecordById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WasteLossRecordBatchFinishedProductsDTO>> UpdateWasteLossRecord(int id, Mystefy.Models.WasteLossRecordBatchFinishedProducts updatedRecord)
        {
            if (id != updatedRecord.Id)
                return BadRequest("Record ID mismatch.");

            var record = await _batchFinishedProductsService.UpdateWasteLossRecordAsync(updatedRecord);
            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss
            };

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WasteLossRecordBatchFinishedProductsDTO>> DeleteWasteLossRecord(int id)
        {
            var deletedRecord = await _batchFinishedProductsService.DeleteWasteLossRecordAsync(id);
            if (deletedRecord == null)
                return NotFound();

            var dto = new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = deletedRecord.Id,
                QuantityLoss = deletedRecord.QuantityLoss,
                Reason = deletedRecord.Reason,
                DateOfLoss = deletedRecord.DateOfLoss
            };

            return Ok(dto);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<List<WasteLossRecordBatchFinishedProductsDTO>>> GetWasteLossRecordsByUserId(int userId)
        {
            var records = await _batchFinishedProductsService.GetWasteLossRecordsByUserIdAsync(userId);

            var dtoList = records.Select(record => new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordBatchFinishedProductsUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("ByProduct/{productId}")]
        public async Task<ActionResult<List<WasteLossRecordBatchFinishedProductsDTO>>> GetWasteLossRecordsByProductId(int productId)
        {
            var records = await _batchFinishedProductsService.GetWasteLossRecordsByProductIdAsync(productId);

            var dtoList = records.Select(record => new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                FinishedProduct = record.FinishedProduct != null ? new WasteLossRecordBatchFinishedProductsFinishedProductDTO
                {
                    ProductID = record.FinishedProduct.ProductID,
                    FragranceID = record.FinishedProduct.FragranceID,
                    Quantity = record.FinishedProduct.Quantity
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("ByBatch/{batchId}")]
        public async Task<ActionResult<List<WasteLossRecordBatchFinishedProductsDTO>>> GetWasteLossRecordsByBatchId(int batchId)
        {
            var records = await _batchFinishedProductsService.GetWasteLossRecordsByBatchIdAsync(batchId);

            var dtoList = records.Select(record => new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                Batch = record.Batch != null ? new WasteLossRecordBatchFinishedProductsBatchDTO
                {
                    BatchID = record.Batch.BatchID,
                    ProductionDate = record.Batch.ProductionDate,
                    BatchSize = record.Batch.BatchSize
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("ByWarehouse/{warehouseId}")]
        public async Task<ActionResult<List<WasteLossRecordBatchFinishedProductsDTO>>> GetWasteLossRecordsByWarehouseId(int warehouseId)
        {
            var records = await _batchFinishedProductsService.GetWasteLossRecordsByWarehouseIdAsync(warehouseId);

            var dtoList = records.Select(record => new WasteLossRecordBatchFinishedProductsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                Warehouse = record.Warehouse != null ? new WasteLossRecordBatchFinishedProductsWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            }).ToList();

            return Ok(dtoList);
        }
    }
}
