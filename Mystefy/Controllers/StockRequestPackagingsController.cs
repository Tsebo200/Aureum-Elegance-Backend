using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRequestPackagingsController : ControllerBase
    {
        private readonly IStockRequestPackagingsRepository _repository;

        public StockRequestPackagingsController(IStockRequestPackagingsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/StockRequestPackagings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestPackagingsDTO>>> GetStockRequestPackagings()
        {
            var requests = await _repository.GetAllStockRequestPackagingsAsync();

            var requestDTOs = requests.Select(r => new StockRequestPackagingsDTO
            {
                Id = r.Id,
                AmountRequested = r.AmountRequested,
                Status = r.Status.ToString(),
                RequestDate = r.RequestDate,
                User = r.User != null ? new StockRequestPackagingsUserDTO
                {
                    UserId = r.User.UserId,
                    Name = r.User.Name,
                    Role = r.User.Role.ToString()
                } : null,
                Ingredients = r.Ingredients != null ? new StockRequestPackagingsIngredientDTO
                {
                    Id = r.Ingredients.Id,
                    Name = r.Ingredients.Name,
                    Type = r.Ingredients.Type,
                    Cost = r.Ingredients.Cost,
                    IsExpired = r.Ingredients.IsExpired
                } : null,
                Warehouse = r.Warehouse != null ? new StockRequestPackagingsWarehouseDTO
                {
                    WarehouseID = r.Warehouse.WarehouseID,
                    Name = r.Warehouse.Name,
                    Location = r.Warehouse.Location
                } : null
            }).ToList();

            return Ok(requestDTOs);
        }

        // GET: api/StockRequestPackagings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestPackagingsDTO>> GetStockRequestPackagings(int id)
        {
            var request = await _repository.GetStockRequestPackagingsByIdAsync(id);

            if (request == null)
                return NotFound();

            var dto = new StockRequestPackagingsDTO
            {
                Id = request.Id,
                AmountRequested = request.AmountRequested,
                Status = request.Status.ToString(),
                RequestDate = request.RequestDate,
                User = request.User != null ? new StockRequestPackagingsUserDTO
                {
                    UserId = request.User.UserId,
                    Name = request.User.Name,
                    Role = request.User.Role.ToString()
                } : null,
                Ingredients = request.Ingredients != null ? new StockRequestPackagingsIngredientDTO
                {
                    Id = request.Ingredients.Id,
                    Name = request.Ingredients.Name,
                    Type = request.Ingredients.Type,
                    Cost = request.Ingredients.Cost,
                    IsExpired = request.Ingredients.IsExpired
                } : null,
                Warehouse = request.Warehouse != null ? new StockRequestPackagingsWarehouseDTO
                {
                    WarehouseID = request.Warehouse.WarehouseID,
                    Name = request.Warehouse.Name,
                    Location = request.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        // POST: api/StockRequestPackagings
        [HttpPost]
        public async Task<ActionResult<StockRequestPackagingsDTO>> PostStockRequestPackagings(StockRequestPackagings stockRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateStockRequestPackagingsAsync(stockRequest);

            var dto = new StockRequestPackagingsDTO
            {
                Id = created.Id,
                AmountRequested = created.AmountRequested,
                Status = created.Status.ToString(),
                RequestDate = created.RequestDate,
                User = created.User != null ? new StockRequestPackagingsUserDTO
                {
                    UserId = created.User.UserId,
                    Name = created.User.Name,
                    Role = created.User.Role.ToString()
                } : null,
                Ingredients = created.Ingredients != null ? new StockRequestPackagingsIngredientDTO
                {
                    Id = created.Ingredients.Id,
                    Name = created.Ingredients.Name,
                    Type = created.Ingredients.Type,
                    Cost = created.Ingredients.Cost,
                    IsExpired = created.Ingredients.IsExpired
                } : null,
                Warehouse = created.Warehouse != null ? new StockRequestPackagingsWarehouseDTO
                {
                    WarehouseID = created.Warehouse.WarehouseID,
                    Name = created.Warehouse.Name,
                    Location = created.Warehouse.Location
                } : null
            };

            return CreatedAtAction(nameof(GetStockRequestPackagings), new { id = created.Id }, dto);
        }

        // PUT: api/StockRequestPackagings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequestPackagings(int id, StockRequestPackagings stockRequest)
        {
            if (id != stockRequest.Id)
                return BadRequest();

            var updated = await _repository.UpdateStockRequestPackagingsAsync(stockRequest);

            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/StockRequestPackagings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequestPackagings(int id)
        {
            var deleted = await _repository.DeleteStockRequestPackagingsAsync(id);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
