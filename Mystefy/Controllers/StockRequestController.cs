using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRequestController : ControllerBase
    {
        private readonly IStockRequestRepository _repository;

        public StockRequestController(IStockRequestRepository repository)
        {
            _repository = repository;
        }

        // GET: api/StockRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestDTO>>> GetStockRequest()
        {
            var stockRequests = await _repository.GetAllStockRequestsAsync();

            var stockRequestDTOs = stockRequests.Select(s => new StockRequestDTO
            {
                Id = s.Id,
                AmountRequested = s.AmountRequested,
                Status = s.Status,
                RequestDate = s.RequestDate,
                User = s.User != null ? new StockRequestUserDTO
                {
                    UserId = s.User.UserId,
                    Name = s.User.Name,
                    Role = s.User.Role.ToString()
                } : null,
                Ingredients = s.Ingredients != null ? new StockRequestIngredientDTO
                {
                    Id = s.Ingredients.Id,
                    Name = s.Ingredients.Name,
                    Type = s.Ingredients.Type,
                    Cost = s.Ingredients.Cost,
                    IsExpired = s.Ingredients.IsExpired
                } : null,
                Warehouse = s.Warehouse != null ? new StockRequestWarehouseDTO
                {
                    WarehouseID = s.Warehouse.WarehouseID,
                    Name = s.Warehouse.Name,
                    Location = s.Warehouse.Location
                } : null
            }).ToList();

            return Ok(stockRequestDTOs);
        }

        // GET: api/StockRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestDTO>> GetStockRequest(int id)
        {
            var stockRequest = await _repository.GetStockRequestByIdAsync(id);
            if (stockRequest == null)
                return NotFound();

            var dto = new StockRequestDTO
            {
                Id = stockRequest.Id,
                AmountRequested = stockRequest.AmountRequested,
                Status = stockRequest.Status,
                RequestDate = stockRequest.RequestDate,
                User = stockRequest.User != null ? new StockRequestUserDTO
                {
                    UserId = stockRequest.User.UserId,
                    Name = stockRequest.User.Name,
                    Role = stockRequest.User.Role.ToString()
                } : null,
                Ingredients = stockRequest.Ingredients != null ? new StockRequestIngredientDTO
                {
                    Id = stockRequest.Ingredients.Id,
                    Name = stockRequest.Ingredients.Name,
                    Type = stockRequest.Ingredients.Type,
                    Cost = stockRequest.Ingredients.Cost,
                    IsExpired = stockRequest.Ingredients.IsExpired
                } : null,
                Warehouse = stockRequest.Warehouse != null ? new StockRequestWarehouseDTO
                {
                    WarehouseID = stockRequest.Warehouse.WarehouseID,
                    Name = stockRequest.Warehouse.Name,
                    Location = stockRequest.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        // POST: api/StockRequest
        [HttpPost]
        public async Task<ActionResult<StockRequestDTO>> PostStockRequest(StockRequest stockRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateStockRequestAsync(stockRequest);

            var dto = new StockRequestDTO
            {
                Id = created.Id,
                AmountRequested = created.AmountRequested,
                Status = created.Status,
                RequestDate = created.RequestDate,
                User = created.User != null ? new StockRequestUserDTO
                {
                    UserId = created.User.UserId,
                    Name = created.User.Name,
                    Role = created.User.Role.ToString()
                } : null,
                Ingredients = created.Ingredients != null ? new StockRequestIngredientDTO
                {
                    Id = created.Ingredients.Id,
                    Name = created.Ingredients.Name,
                    Type = created.Ingredients.Type,
                    Cost = created.Ingredients.Cost,
                    IsExpired = created.Ingredients.IsExpired
                } : null,
                Warehouse = created.Warehouse != null ? new StockRequestWarehouseDTO
                {
                    WarehouseID = created.Warehouse.WarehouseID,
                    Name = created.Warehouse.Name,
                    Location = created.Warehouse.Location
                } : null
            };

            return CreatedAtAction(nameof(GetStockRequest), new { id = created.Id }, dto);
        }

        // PUT: api/StockRequest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequest(int id, StockRequest stockRequest)
        {
            if (id != stockRequest.Id)
                return BadRequest();

            var updated = await _repository.UpdateStockRequestAsync(stockRequest);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/StockRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequest(int id)
        {
            var deleted = await _repository.DeleteStockRequestAsync(id);
            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
