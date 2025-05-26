using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WasteLossRecordIngredientsController : ControllerBase
    {
        private readonly IWasteLossRecordIngredientsRepository _repository;

        public WasteLossRecordIngredientsController(IWasteLossRecordIngredientsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WasteLossRecordIngredientsDTO>>> GetWasteLossRecordIngredients()
        {
            var records = await _repository.GetAllWasteLossRecordsAsync();

            var recordDTOs = records.Select(r => new WasteLossRecordIngredientsDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = r.Ingredients.Id,
                    Name = r.Ingredients.Name,
                    Type = r.Ingredients.Type,
                    Cost = r.Ingredients.Cost,
                    IsExpired = r.Ingredients.IsExpired
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WasteLossRecordIngredientsDTO>> GetWasteLossRecordIngredient(int id)
        {
            var record = await _repository.GetWasteLossRecordByIdAsync(id);

            if (record == null)
                return NotFound();

            var dto = new WasteLossRecordIngredientsDTO
            {
                Id = record.Id,
                QuantityLoss = record.QuantityLoss,
                Reason = record.Reason,
                DateOfLoss = record.DateOfLoss,
                User = record.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = record.User.UserId,
                    Name = record.User.Name,
                    Role = record.User.Role.ToString()
                } : null,
                Ingredients = record.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = record.Ingredients.Id,
                    Name = record.Ingredients.Name,
                    Type = record.Ingredients.Type,
                    Cost = record.Ingredients.Cost,
                    IsExpired = record.Ingredients.IsExpired
                } : null,
                Warehouse = record.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
                {
                    WarehouseID = record.Warehouse.WarehouseID,
                    Name = record.Warehouse.Name,
                    Location = record.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WasteLossRecordIngredientsDTO>> PostWasteLossRecordIngredient(WasteLossRecordIngredients record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateWasteLossRecordAsync(record);

            var dto = new WasteLossRecordIngredientsDTO
            {
                Id = created.Id,
                QuantityLoss = created.QuantityLoss,
                Reason = created.Reason,
                DateOfLoss = created.DateOfLoss,
                User = created.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = created.User.UserId,
                    Name = created.User.Name,
                    Role = created.User.Role.ToString()
                } : null,
                Ingredients = created.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = created.Ingredients.Id,
                    Name = created.Ingredients.Name,
                    Type = created.Ingredients.Type,
                    Cost = created.Ingredients.Cost,
                    IsExpired = created.Ingredients.IsExpired
                } : null,
                Warehouse = created.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
                {
                    WarehouseID = created.Warehouse.WarehouseID,
                    Name = created.Warehouse.Name,
                    Location = created.Warehouse.Location
                } : null
            };

            return CreatedAtAction(nameof(GetWasteLossRecordIngredient), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWasteLossRecordIngredient(int id, WasteLossRecordIngredients record)
        {
            if (id != record.Id)
                return BadRequest("ID mismatch.");

            var updated = await _repository.UpdateWasteLossRecordAsync(record);

            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWasteLossRecordIngredient(int id)
        {
            var deleted = await _repository.DeleteWasteLossRecordAsync(id);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordIngredientsDTO>>> GetByUser(int userId)
        {
            var records = await _repository.GetWasteLossRecordsByUserIdAsync(userId);

            var recordDTOs = records.Select(r => new WasteLossRecordIngredientsDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = r.Ingredients.Id,
                    Name = r.Ingredients.Name,
                    Type = r.Ingredients.Type,
                    Cost = r.Ingredients.Cost,
                    IsExpired = r.Ingredients.IsExpired
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("ingredient/{ingredientId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordIngredientsDTO>>> GetByIngredient(int ingredientId)
        {
            var records = await _repository.GetWasteLossRecordsByIngredientIdAsync(ingredientId);

            var recordDTOs = records.Select(r => new WasteLossRecordIngredientsDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = r.Ingredients.Id,
                    Name = r.Ingredients.Name,
                    Type = r.Ingredients.Type,
                    Cost = r.Ingredients.Cost,
                    IsExpired = r.Ingredients.IsExpired
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(recordDTOs);
        }

        [HttpGet("warehouse/{warehouseId}")]
        public async Task<ActionResult<IEnumerable<WasteLossRecordIngredientsDTO>>> GetByWarehouse(int warehouseId)
        {
            var records = await _repository.GetWasteLossRecordsByWarehouseIdAsync(warehouseId);

            var recordDTOs = records.Select(r => new WasteLossRecordIngredientsDTO
            {
                Id = r.Id,
                QuantityLoss = r.QuantityLoss,
                Reason = r.Reason,
                DateOfLoss = r.DateOfLoss,
                User = r.User != null ? new WasteLossRecordIngredientsUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Ingredients != null ? new WasteLossRecordIngredientsIngredientsDTO
                {
                    Id = r.Ingredients.Id,
                    Name = r.Ingredients.Name,
                    Type = r.Ingredients.Type,
                    Cost = r.Ingredients.Cost,
                    IsExpired = r.Ingredients.IsExpired
                } : null,
                Warehouse = r.Warehouse != null ? new WasteLossRecordIngredientsWarehouseDTO
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
