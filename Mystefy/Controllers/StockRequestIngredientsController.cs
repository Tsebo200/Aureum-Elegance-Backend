using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRequestIngredientsController : ControllerBase
    {
        private readonly IStockRequestIngredientsRepository _repository;

        public StockRequestIngredientsController(IStockRequestIngredientsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/StockRequestIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockRequestIngredientsDTO>>> GetStockRequestIngredients()
        {
            var requests = await _repository.GetAllStockRequestIngredientsAsync();

            var requestDTOs = requests.Select(s => new StockRequestIngredientsDTO
            {
                Id = s.Id,
                AmountRequested = s.AmountRequested,
                Status = s.Status.ToString(),
                RequestDate = s.RequestDate,
                User = s.User != null ? new StockRequestIngredientsUserDTO
                {
                    UserId = s.User.UserId,
                    Name = s.User.Name,
                    Role = s.User.Role.ToString()
                } : null,
                Ingredients = s.Ingredients != null ? new StockRequestIngredientsIngredientDTO
                {
                    Id = s.Ingredients.Id,
                    Name = s.Ingredients.Name,
                    Type = s.Ingredients.Type,
                    Cost = s.Ingredients.Cost,
                    IsExpired = s.Ingredients.IsExpired
                } : null,
                Warehouse = s.Warehouse != null ? new StockRequestIngredientsWarehouseDTO
                {
                    WarehouseID = s.Warehouse.WarehouseID,
                    Name = s.Warehouse.Name,
                    Location = s.Warehouse.Location
                } : null
            }).ToList();

            return Ok(requestDTOs);
        }

        // GET: api/StockRequestIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestIngredientsDTO>> GetStockRequestIngredients(int id)
        {
            var stockRequest = await _repository.GetStockRequestIngredientsByIdAsync(id);

            if (stockRequest == null)
                return NotFound();

            var dto = new StockRequestIngredientsDTO
            {
                Id = stockRequest.Id,
                AmountRequested = stockRequest.AmountRequested,
                Status = stockRequest.Status.ToString(),
                RequestDate = stockRequest.RequestDate,
                User = stockRequest.User != null ? new StockRequestIngredientsUserDTO
                {
                    UserId = stockRequest.User.UserId,
                    Name = stockRequest.User.Name,
                    Role = stockRequest.User.Role.ToString()
                } : null,
                Ingredients = stockRequest.Ingredients != null ? new StockRequestIngredientsIngredientDTO
                {
                    Id = stockRequest.Ingredients.Id,
                    Name = stockRequest.Ingredients.Name,
                    Type = stockRequest.Ingredients.Type,
                    Cost = stockRequest.Ingredients.Cost,
                    IsExpired = stockRequest.Ingredients.IsExpired
                } : null,
                Warehouse = stockRequest.Warehouse != null ? new StockRequestIngredientsWarehouseDTO
                {
                    WarehouseID = stockRequest.Warehouse.WarehouseID,
                    Name = stockRequest.Warehouse.Name,
                    Location = stockRequest.Warehouse.Location
                } : null
            };

            return Ok(dto);
        }

        // POST: api/StockRequestIngredients
        [HttpPost]
        public async Task<ActionResult<StockRequestIngredientsDTO>> PostStockRequestIngredients(StockRequestIngredients stockRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repository.CreateStockRequestIngredientsAsync(stockRequest);

            // Map to DTO
            var dto = new StockRequestIngredientsDTO
            {
                Id = created.Id,
                AmountRequested = created.AmountRequested,
                Status = created.Status.ToString(),
                RequestDate = created.RequestDate,
                User = created.User != null ? new StockRequestIngredientsUserDTO
                {
                    UserId = created.User.UserId,
                    Name = created.User.Name,
                    Role = created.User.Role.ToString()
                } : null,
                Ingredients = created.Ingredients != null ? new StockRequestIngredientsIngredientDTO
                {
                    Id = created.Ingredients.Id,
                    Name = created.Ingredients.Name,
                    Type = created.Ingredients.Type,
                    Cost = created.Ingredients.Cost,
                    IsExpired = created.Ingredients.IsExpired
                } : null,
                Warehouse = created.Warehouse != null ? new StockRequestIngredientsWarehouseDTO
                {
                    WarehouseID = created.Warehouse.WarehouseID,
                    Name = created.Warehouse.Name,
                    Location = created.Warehouse.Location
                } : null
            };

            return CreatedAtAction(nameof(GetStockRequestIngredients), new { id = created.Id }, dto);
        }

        // PUT: api/StockRequestIngredients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequestIngredients(int id, StockRequestIngredients stockRequest)
        {
            if (id != stockRequest.Id)
                return BadRequest();

            var updated = await _repository.UpdateStockRequestIngredientsAsync(stockRequest);

            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/StockRequestIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequestIngredients(int id)
        {
            var deleted = await _repository.DeleteStockRequestIngredientsAsync(id);
            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
