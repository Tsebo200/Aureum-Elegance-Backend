using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Models;
using Mystefy.Interfaces;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagingController : ControllerBase
    {
        private readonly IPackagingRepository _packagingRepo;

        public PackagingController(IPackagingRepository packagingRepo)
        {
            _packagingRepo = packagingRepo;
        }

        // GET: api/Packaging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackagingDTO>>> GetPackaging()
        {
            // You can expand this with a new method in the repo later if needed - A
            var packagingList = await _packagingRepo.GetAllPackagingAsync();

            var dtoList = packagingList.Select(p => new PackagingDTO
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Stock = p.Stock,
                FinishedProduct = p.FinishedProduct?.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            });

            return Ok(dtoList);
        }

        // GET: api/Packaging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackagingDTO>> GetPackaging(int id)
        {
            var packaging = await _packagingRepo.GetPackagingWithDetailsAsync(id);
            if (packaging == null)
            {
                return NotFound();
            }

            var dto = new PackagingDTO
            {
                Id = packaging.Id,
                Name = packaging.Name,
                Type = packaging.Type,
                Stock = packaging.Stock,
                FinishedProduct = packaging.FinishedProduct?.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            };

            return Ok(dto);
        }

        // POST: api/Packaging
        [HttpPost]
        public async Task<ActionResult<PackagingDTO>> PostPackaging(Packaging packaging)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _packagingRepo.CreatePackagingAsync(packaging);

            var dto = new PackagingDTO
            {
                Id = created.Id,
                Name = created.Name,
                Type = created.Type,
                Stock = created.Stock,
                FinishedProduct = created.FinishedProduct?.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            };

            return CreatedAtAction(nameof(GetPackaging), new { id = created.Id }, dto);
        }

        // PUT: api/Packaging/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackaging(int id, Packaging packaging)
        {
            if (id != packaging.Id)
                return BadRequest();

            var updated = await _packagingRepo.UpdatePackagingAsync(packaging);

            if (updated == null)
                return NotFound();

            var dto = new PackagingDTO
            {
                Id = updated.Id,
                Name = updated.Name,
                Type = updated.Type,
                Stock = updated.Stock,
                FinishedProduct = updated.FinishedProduct?.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            };

            return Ok(dto);
        }

        // DELETE: api/Packaging/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var deleted = await _packagingRepo.DeletePackagingAsync(id);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
