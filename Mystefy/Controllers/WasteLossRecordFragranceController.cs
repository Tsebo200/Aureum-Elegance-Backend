using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteLossRecordFragranceController : ControllerBase
    {
        private readonly IWasteLossRecordFragranceRepository _fragranceLossService;

        public WasteLossRecordFragranceController(IWasteLossRecordFragranceRepository fragranceLossService)
        {
            _fragranceLossService = fragranceLossService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WasteLossRecordFragranceDTO>>> GetAllWasteLossRecords()
        {
            var records = await _fragranceLossService.GetAllWasteLossRecordsAsync();

            var dtoList = records.Select(record => new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordFragranceUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                Fragrance = record.Fragrance != null ? new WasteLossRecordIngredientsFragranceDTO
                {
                    Id = record.Fragrance.Id,
                    Name = record.Fragrance.Name,
                    Description = record.Fragrance.Description,
                    Cost = record.Fragrance.Cost,
                    ExpiryDate = record.Fragrance.ExpiryDate,
                    Volume = record.Fragrance.Volume
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordFragranceWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WasteLossRecordFragranceDTO>> GetWasteLossRecordById(int id)
        {
            var record = await _fragranceLossService.GetWasteLossRecordByIdAsync(id);

            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordFragranceUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                Fragrance = record.Fragrance != null ? new WasteLossRecordIngredientsFragranceDTO
                {
                    Id = record.Fragrance.Id,
                    Name = record.Fragrance.Name,
                    Description = record.Fragrance.Description,
                    Cost = record.Fragrance.Cost,
                    ExpiryDate = record.Fragrance.ExpiryDate,
                    Volume = record.Fragrance.Volume
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordFragranceWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WasteLossRecordFragranceDTO>> CreateWasteLossRecord(WasteLossRecordFragrance record)
        {
            var createdRecord = await _fragranceLossService.CreateWasteLossRecordAsync(record);

            var dto = new WasteLossRecordFragranceDTO
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
        public async Task<ActionResult<WasteLossRecordFragranceDTO>> UpdateWasteLossRecord(int id, WasteLossRecordFragrance updatedRecord)
        {
            if (id != updatedRecord.Id)
                return BadRequest("Record ID mismatch.");

            var record = await _fragranceLossService.UpdateWasteLossRecordAsync(updatedRecord);
            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss
            };

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<WasteLossRecordFragranceDTO>> DeleteWasteLossRecord(int id)
        {
            var deletedRecord = await _fragranceLossService.DeleteWasteLossRecordAsync(id);
            if (deletedRecord == null)
                return NotFound();

            var dto = new WasteLossRecordFragranceDTO
            {
                Id = deletedRecord.Id,
                QuantityLoss = deletedRecord.QuantityLoss,
                Reason = deletedRecord.Reason,
                DateOfLoss = deletedRecord.DateOfLoss
            };

            return Ok(dto);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<List<WasteLossRecordFragranceDTO>>> GetWasteLossRecordsByUserId(int userId)
        {
            var records = await _fragranceLossService.GetWasteLossRecordsByUserIdAsync(userId);

            var dtoList = records.Select(record => new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordFragranceUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("ByFragrance/{fragranceId}")]
        public async Task<ActionResult<List<WasteLossRecordFragranceDTO>>> GetWasteLossRecordsByFragranceId(int fragranceId)
        {
            var records = await _fragranceLossService.GetWasteLossRecordsByFragranceIdAsync(fragranceId);

            var dtoList = records.Select(record => new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                Fragrance = record.Fragrance != null ? new WasteLossRecordIngredientsFragranceDTO
                {
                    Id = record.Fragrance.Id,
                    Name = record.Fragrance.Name,
                    Description = record.Fragrance.Description,
                    Cost = record.Fragrance.Cost,
                    ExpiryDate = record.Fragrance.ExpiryDate,
                    Volume = record.Fragrance.Volume
                } : null
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("ByWarehouse/{warehouseId}")]
        public async Task<ActionResult<List<WasteLossRecordFragranceDTO>>> GetWasteLossRecordsByWarehouseId(int warehouseId)
        {
            var records = await _fragranceLossService.GetWasteLossRecordsByWarehouseIdAsync(warehouseId);

            var dtoList = records.Select(record => new WasteLossRecordFragranceDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                Warehouse = record.Warehouse != null ? new WasteLossRecordFragranceWarehouseDTO
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
