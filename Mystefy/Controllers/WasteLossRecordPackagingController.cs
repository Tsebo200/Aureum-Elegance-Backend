using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteLossRecordPackagingController : ControllerBase
    {
        private readonly IWasteLossRecordPackagingRepository _repository;

        public WasteLossRecordPackagingController(IWasteLossRecordPackagingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasteLossRecordPackagingDTO>>> GetWasteLossRecordPackagings()
        {
            var records = await _repository.GetAllWasteLossRecordsAsync();

            var recordDTOs = records.Select(r => new WasteLossRecordPackagingDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = r.Packaging.Id,
                    Name = r.Packaging.Name,
                    Type = r.Packaging.Type,
                    Stock = r.Packaging.Stock,
                    // FinishedProduct = r.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = r.Packaging.FinishedProduct.Id,
                    //     // Name = r.Packaging.FinishedProduct.Name,
                    //     // Description = r.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WasteLossRecordPackagingDTO>> GetWasteLossRecordPackaging(int id)
        {
            var record = await _repository.GetWasteLossRecordByIdAsync(id);

            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordPackagingDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                Ingredients = record.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = record.Packaging.Id,
                    Name = record.Packaging.Name,
                    Type = record.Packaging.Type,
                    Stock = record.Packaging.Stock,
                    // FinishedProduct = record.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = record.Packaging.FinishedProduct.Id,
                    //     // Name = record.Packaging.FinishedProduct.Name,
                    //     // Description = record.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WasteLossRecordPackagingDTO>> PostWasteLossRecordPackaging(WasteLossRecordPackaging record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateWasteLossRecordAsync(record);

            var dto = new WasteLossRecordPackagingDTO
            {
                Id = created.Id,
                QuantityLoss = created.QuantityLoss,
                Reason = created.Reason,
                DateOfLoss = created.DateOfLoss,
                User = created.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = created.User.UserId,
                    Name = created.User.Name,
                    Role = created.User.Role.ToString()
                } : null,
                Ingredients = created.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = created.Packaging.Id,
                    Name = created.Packaging.Name,
                    Type = created.Packaging.Type,
                    Stock = created.Packaging.Stock,
                    // FinishedProduct = created.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = created.Packaging.FinishedProduct.Id,
                    //     // Name = created.Packaging.FinishedProduct.Name,
                    //     // Description = created.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = created.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = created.Warehouse.WarehouseID,
                    Name = created.Warehouse.Name,
                    Location = created.Warehouse.Location
                } : null
            };

            return CreatedAtAction(nameof(GetWasteLossRecordPackaging), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasteLossRecordPackaging(int id, WasteLossRecordPackaging record)
        {
            if (id != record.Id)
                return BadRequest("ID mismatch.");

            var updated = await _repository.UpdateWasteLossRecordAsync(record);

            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteLossRecordPackaging(int id)
        {
            var deleted = await _repository.DeleteWasteLossRecordAsync(id);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordPackagingDTO>>> GetByUser(int userId)
        {
            var records = await _repository.GetWasteLossRecordsByUserIdAsync(userId);

            var recordDTOs = records.Select(r => new WasteLossRecordPackagingDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = r.Packaging.Id,
                    Name = r.Packaging.Name,
                    Type = r.Packaging.Type,
                    Stock = r.Packaging.Stock,
                    // FinishedProduct = r.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = r.Packaging.FinishedProduct.Id,
                    //     // Name = r.Packaging.FinishedProduct.Name,
                    //     // Description = r.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("packaging/{packagingId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordPackagingDTO>>> GetByPackaging(int packagingId)
        {
            var records = await _repository.GetWasteLossRecordsByPackagingIdAsync(packagingId);

            var recordDTOs = records.Select(r => new WasteLossRecordPackagingDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = r.Packaging.Id,
                    Name = r.Packaging.Name,
                    Type = r.Packaging.Type,
                    Stock = r.Packaging.Stock,
                    // FinishedProduct = r.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = r.Packaging.FinishedProduct.Id,
                    //     // Name = r.Packaging.FinishedProduct.Name,
                    //     // Description = r.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordPackagingDTO>>> GetByWarehouse(int warehouseId)
        {
            var records = await _repository.GetWasteLossRecordsByWarehouseIdAsync(warehouseId);

            var recordDTOs = records.Select(r => new WasteLossRecordPackagingDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordPackagingUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Packaging != null ? new WasteLossRecordPackagingPackagingDTO
                {
                    Id = r.Packaging.Id,
                    Name = r.Packaging.Name,
                    Type = r.Packaging.Type,
                    Stock = r.Packaging.Stock,
                    // FinishedProduct = r.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                    // {
                    //     // Id = r.Packaging.FinishedProduct.Id,
                    //     // Name = r.Packaging.FinishedProduct.Name,
                    //     // Description = r.Packaging.FinishedProduct.Description
                    // } : null
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordPackagingWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }
    }
}
