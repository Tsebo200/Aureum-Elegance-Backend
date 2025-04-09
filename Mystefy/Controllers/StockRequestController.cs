using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockRequestController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public StockRequestController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/StockRequest
        [HttpGet]
        // This is the Get Request with DTO...3 Stock Request DTOs 
        // being StockRequestUserDTO and StockRequestIngredientDTO and StockRequestWarehouseDTO integrated all in the Stock Request
        public async Task<ActionResult<IEnumerable<StockRequestDTO>>> GetStockRequest()
        {
            var stockRequests = await _context.StockRequests
                .Include(s => s.User)
                .Include(s => s.Ingredients)
                .Include(s => s.Warehouse)
                .Select(s => new StockRequestDTO
                {
                    Id = s.Id,
                    AmountRequested = s.AmountRequested,
                    Status = s.Status,
                    RequestDate = s.RequestDate,
                    // Mapping out the DTO from StockRequestUserDTO
                    User = s.User != null ? new StockRequestUserDTO
                    {
                        UserId = s.User.UserId,
                        Name = s.User.Name,
                        Role = s.User.Role.ToString()
                    } : null,
                    // Mapping out the DTO from StockRequestIngredientDTO
                    Ingredients = s.Ingredients != null ? new StockRequestIngredientDTO
                    {
                        Id = s.Ingredients.Id,
                        Name = s.Ingredients.Name,
                        Type = s.Ingredients.Type,
                        Cost = s.Ingredients.Cost,
                        IsExpired = s.Ingredients.IsExpired
                    } : null,
                    // Mapping out the DTO from StockRequestWarehouseDTO
                    Warehouse = s.Warehouse != null ? new StockRequestWarehouseDTO
                    {
                        WarehouseID = s.Warehouse.WarehouseID,
                        Name = s.Warehouse.Name,
                        Location = s.Warehouse.Location
                    } : null
                })
                .ToListAsync();

            return Ok(stockRequests);
        }

        // GET: api/StockRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockRequestDTO>> GetStockRequest(int id)
        {
            var stockRequest = await _context.StockRequests
                .Include(s => s.User)
                .Include(s => s.Ingredients)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (stockRequest == null)
            {
                return NotFound();
            }

            var stockRequestDTO = new StockRequestDTO
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

            return Ok(stockRequestDTO);
        }

        // PUT: api/StockRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockRequest(int id, StockRequest stockRequest)
        {
            if (id != stockRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockRequest
        [HttpPost]
        public async Task<ActionResult<StockRequestDTO>> PostStockRequest(StockRequest stockRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verify that the referenced entities exist
            var ingredientExists = await _context.Ingredients.AnyAsync(i => i.Id == stockRequest.IngredientsId);
            var warehouseExists = await _context.Warehouses.AnyAsync(w => w.WarehouseID == stockRequest.WarehouseId);
            
            if (!ingredientExists)
            {
                ModelState.AddModelError("IngredientsId", "The specified Ingredient does not exist.");
                return BadRequest(ModelState);
            }

            if (!warehouseExists)
            {
                ModelState.AddModelError("WarehouseId", "The specified Warehouse does not exist.");
                return BadRequest(ModelState);
            }

            if (stockRequest.UserId.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.UserId == stockRequest.UserId);
                if (!userExists)
                {
                    ModelState.AddModelError("UserId", "The specified User does not exist.");
                    return BadRequest(ModelState);
                }
            }

            _context.StockRequests.Add(stockRequest);
            await _context.SaveChangesAsync();

            // Load related entities
            await _context.Entry(stockRequest)
                .Reference(s => s.User)
                .LoadAsync();
            await _context.Entry(stockRequest)
                .Reference(s => s.Ingredients)
                .LoadAsync();
            await _context.Entry(stockRequest)
                .Reference(s => s.Warehouse)
                .LoadAsync();

            // Convert to DTO
            var stockRequestDTO = new StockRequestDTO
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

            return CreatedAtAction(nameof(GetStockRequest), new { id = stockRequest.Id }, stockRequestDTO);
        }

        // DELETE: api/StockRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockRequest(int id)
        {
            var stockRequest = await _context.StockRequests.FindAsync(id);
            if (stockRequest == null)
            {
                return NotFound();
            }

            _context.StockRequests.Remove(stockRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockRequestExists(int id)
        {
            return _context.StockRequests.Any(e => e.Id == id);
        }
    }
}
