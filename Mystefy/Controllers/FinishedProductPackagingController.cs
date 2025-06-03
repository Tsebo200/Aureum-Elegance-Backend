using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishedProductPackagingController : ControllerBase
    {
        private readonly IFinishedProductPackaging _finishedProductPackaging;

        public FinishedProductPackagingController(IFinishedProductPackaging finishedProductPackaging)
        {
            _finishedProductPackaging = finishedProductPackaging;
        }

        // GET: api/FinishedProductPackaging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinishedProductPackageDTO>>> GetAll()
        {
            var result = await _finishedProductPackaging.GetAllFinishedProductPackaging();

            var dtoList = result.Select(fpp => new FinishedProductPackageDTO
            {
                ProductID = fpp.ProductID,
                PackagingId = fpp.PackagingID,
                Amount = fpp.Amount,
                FinishedProduct = fpp.FinishedProduct == null ? null : new IncludeFinishedProductInFinishedProductPackagingDTO
                {
                    ProductID = fpp.FinishedProduct.ProductID,
                    FragranceID = fpp.FinishedProduct.FragranceID,
                    ProductName = fpp.FinishedProduct.ProductName,
                    Quantity = fpp.FinishedProduct.Quantity
                },
                Packaging = fpp.Packaging == null ? null : new IncludePackagingInFinishedProductPackagingDTO
                {
                    Id = fpp.Packaging.Id,
                    Name = fpp.Packaging.Name,
                    Type = fpp.Packaging.Type,
                    Stock = fpp.Packaging.Stock
                }
            });

            return Ok(dtoList);
        }

        // GET: api/FinishedProductPackaging/{productId}/{packagingId}
        [HttpGet("{productId}/{packagingId}")]
        public async Task<ActionResult<FinishedProductPackageDTO>> GetById(int productId, int packagingId)
        {
            var fpp = await _finishedProductPackaging.GetFinishedProductPackagingById(productId, packagingId);
            if (fpp == null) return NotFound();

            var dto = new FinishedProductPackageDTO
            {
                ProductID = fpp.ProductID,
                PackagingId = fpp.PackagingID,
                Amount = fpp.Amount,
                FinishedProduct = fpp.FinishedProduct == null ? null : new IncludeFinishedProductInFinishedProductPackagingDTO
                {
                    ProductID = fpp.FinishedProduct.ProductID,
                    FragranceID = fpp.FinishedProduct.FragranceID,
                    ProductName = fpp.FinishedProduct.ProductName,
                    Quantity = fpp.FinishedProduct.Quantity
                },
                Packaging = fpp.Packaging == null ? null : new IncludePackagingInFinishedProductPackagingDTO
                {
                    Id = fpp.Packaging.Id,
                    Name = fpp.Packaging.Name,
                    Type = fpp.Packaging.Type,
                    Stock = fpp.Packaging.Stock
                }
            };

            return Ok(dto);
        }

        // POST: api/FinishedProductPackaging
        [HttpPost]
        public async Task<ActionResult<FinishedProductPackaging>> Post([FromBody] PostFinishedProductPackagingDTO dto)
        {
            var model = new FinishedProductPackaging
            {
                ProductID = dto.ProductID,
                PackagingID = dto.PackagingId,
                Amount = dto.Amount
            };

            var created = await _finishedProductPackaging.AddFinishedProductPackaging(model);

            return CreatedAtAction(
                nameof(GetById),
                new { productId = created.ProductID, packagingId = created.PackagingID },
                created
            );
        }

        // PUT: api/FinishedProductPackaging/{productId}/{packagingId}
        [HttpPut("{productId}/{packagingId}")]
        public async Task<IActionResult> Put(int productId, int packagingId, [FromBody] PostFinishedProductPackagingDTO dto)
        {
            if (productId != dto.ProductID || packagingId != dto.PackagingId)
            {
                return BadRequest("ProductID and PackagingId in URL must match body.");
            }

            var updateModel = new FinishedProductPackaging
            {
                ProductID = dto.ProductID,
                PackagingID = dto.PackagingId,
                Amount = dto.Amount
            };

            var updated = await _finishedProductPackaging.UpdateFinishedProductPackaging(productId, packagingId, updateModel);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/FinishedProductPackaging/{productId}/{packagingId}
        [HttpDelete("{productId}/{packagingId}")]
        public async Task<IActionResult> Delete(int productId, int packagingId)
        {
            var deleted = await _finishedProductPackaging.DeleteFinishedProductPackaging(productId, packagingId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
