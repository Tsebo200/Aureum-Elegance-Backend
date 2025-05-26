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
            Packaging = r.Packaging != null ? new StockRequestPackagingsPackagingDTO
            {
                Id = r.Packaging.Id,
                Name = r.Packaging.Name,
                Type = r.Packaging.Type,
                Stock = r.Packaging.Stock,
                
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
            Packaging = request.Packaging != null ? new StockRequestPackagingsPackagingDTO
            {
                Id = request.Packaging.Id,
                Name = request.Packaging.Name,
                Type = request.Packaging.Type,
                Stock = request.Packaging.Stock,
                // FinishedProduct = request.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                // {
                //     // Map fields for FinishedProduct here
                // } : null
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
            Packaging = created.Packaging != null ? new StockRequestPackagingsPackagingDTO
            {
                Id = created.Packaging.Id,
                Name = created.Packaging.Name,
                Type = created.Packaging.Type,
                Stock = created.Packaging.Stock,
                // FinishedProduct = created.Packaging.FinishedProduct != null ? new PackagingFinishedProductDTO
                // {
                //     // Map fields for FinishedProduct here
                // } : null
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
