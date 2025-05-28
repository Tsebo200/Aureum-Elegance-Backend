using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishedProductController : ControllerBase
    {
        private readonly IFinishedProductService _finishedProductService;

        public FinishedProductController(IFinishedProductService finishedProductservice)
        {
            _finishedProductService = finishedProductservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinishedProductDTO>>> GetProducts()
        {
            var products = await _finishedProductService.GetAllProductsAsync();

            var productDtos = products.Select(p => new FinishedProductDTO
            {
                ProductID = p.ProductID,
                FragranceID = p.FragranceID,
                ProductName = p.ProductName,
                Quantity = p.Quantity,
                FinishedProductPackaging = p.FinishedProductPackaging?.Select(pp => new GetFinishedProductPackagingDTO
                {
                    ProductID = pp.ProductID,
                    PackagingId = pp.PackagingID,
                    Amount = pp.Amount,
                    Packaging = pp.Packaging != null ? new getPackagingInfoDTO
                    {
                        Id = pp.Packaging.Id,
                        Name = pp.Packaging.Name,
                        Type = pp.Packaging.Type,
                        Stock = pp.Packaging.Stock
                    } : null
                }).ToList()
            }).ToList();

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinishedProductDTO>> GetProduct(int id)
        {
            var product = await _finishedProductService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var dto = new FinishedProductDTO
            {
                ProductID = product.ProductID,
                FragranceID = product.FragranceID,
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                FinishedProductPackaging = product.FinishedProductPackaging?.Select(pp => new GetFinishedProductPackagingDTO
                {
                    ProductID = pp.ProductID,
                    PackagingId = pp.PackagingID,
                    Amount = pp.Amount,
                    Packaging = pp.Packaging != null ? new getPackagingInfoDTO
                    {
                        Id = pp.Packaging.Id,
                        Name = pp.Packaging.Name,
                        Type = pp.Packaging.Type,
                        Stock = pp.Packaging.Stock
                    } : null
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpGet("GetName/{name}")]
        public async Task<ActionResult<FinishedProductDTO>> GetProductByName(string name)
        {
            var product = await _finishedProductService.GetFinishedProductByName(name);
            if (product == null) return NotFound();

            var dto = new FinishedProductDTO
            {
                ProductID = product.ProductID,
                FragranceID = product.FragranceID,
                ProductName = product.ProductName,
                Quantity = product.Quantity,
                FinishedProductPackaging = product.FinishedProductPackaging?.Select(pp => new GetFinishedProductPackagingDTO
                {
                    ProductID = pp.ProductID,
                    PackagingId = pp.PackagingID,
                    Amount = pp.Amount,
                    Packaging = pp.Packaging != null ? new getPackagingInfoDTO
                    {
                        Id = pp.Packaging.Id,
                        Name = pp.Packaging.Name,
                        Type = pp.Packaging.Type,
                        Stock = pp.Packaging.Stock
                    } : null
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] PostFinishedProductDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Product data is required.");

            var product = new FinishedProduct
            {
                ProductID = productDto.ProductID,
                FragranceID = productDto.FragranceID,
                ProductName = productDto.ProductName,
                Quantity = productDto.Quantity
            };

            var created = await _finishedProductService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = created?.ProductID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] PostFinishedProductDTO productDto)
        {
            var product = new FinishedProduct
            {
                ProductID = id,
                FragranceID = productDto.FragranceID,
                ProductName = productDto.ProductName,
                Quantity = productDto.Quantity
            };

            var updated = await _finishedProductService.UpdateProductAsync(product);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _finishedProductService.DeleteProductAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
