using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.DTOs;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagingController : ControllerBase
    {
        private readonly MystefyDbContext _context;

        public PackagingController(MystefyDbContext context)
        {
            _context = context;
        }

        // GET: api/Packaging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackagingDTO>>> GetPackaging()
        {
            var packagingList = await _context.Packaging
                .Include(p => p.FinishedProduct)
                .Select(p => new PackagingDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Type = p.Type,
                    Stock = p.Stock,
                    FinishedProduct = p.FinishedProduct.Select(fp => new PackagingFinishedProductDTO
                    {
                        ProductID = fp.ProductID,
                        FragranceID = fp.FragranceID,
                        PackagingID = fp.PackagingID,
                        Quantity = fp.Quantity
                    }).FirstOrDefault()
                })
                .ToListAsync();

            return Ok(packagingList);
        }

        // GET: api/Packaging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackagingDTO>> GetPackaging(int id)
        {
            var packaging = await _context.Packaging
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packaging == null)
            {
                return NotFound();
            }

            var packagingDTO = new PackagingDTO
            {
                Id = packaging.Id,
                Name = packaging.Name,
                Type = packaging.Type,
                Stock = packaging.Stock,
                FinishedProduct = packaging.FinishedProduct.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            };

            return Ok(packagingDTO);
        }

        // POST: api/Packaging
        [HttpPost]
        public async Task<ActionResult<PackagingDTO>> PostPackaging(Packaging packaging)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Packaging.Add(packaging);
            await _context.SaveChangesAsync();

            // Load related entities
            await _context.Entry(packaging)
                .Collection(p => p.FinishedProduct)
                .LoadAsync();

            // Convert to DTO
            var packagingDTO = new PackagingDTO
            {
                Id = packaging.Id,
                Name = packaging.Name,
                Type = packaging.Type,
                Stock = packaging.Stock,
                FinishedProduct = packaging.FinishedProduct.Select(fp => new PackagingFinishedProductDTO
                {
                    ProductID = fp.ProductID,
                    FragranceID = fp.FragranceID,
                    PackagingID = fp.PackagingID,
                    Quantity = fp.Quantity
                }).FirstOrDefault()
            };

            return CreatedAtAction(nameof(GetPackaging), new { id = packaging.Id }, packagingDTO);
        }

        // PUT: api/Packaging/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackaging(int id, Packaging packaging)
        {
            if (id != packaging.Id)
            {
                return BadRequest();
            }

            _context.Entry(packaging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // Load related entities after update
                await _context.Entry(packaging)
                    .Collection(p => p.FinishedProduct)
                    .LoadAsync();

                var packagingDTO = new PackagingDTO
                {
                    Id = packaging.Id,
                    Name = packaging.Name,
                    Type = packaging.Type,
                    Stock = packaging.Stock,
                    FinishedProduct = packaging.FinishedProduct.Select(fp => new PackagingFinishedProductDTO
                    {
                        ProductID = fp.ProductID,
                        FragranceID = fp.FragranceID,
                        PackagingID = fp.PackagingID,
                        Quantity = fp.Quantity
                    }).FirstOrDefault()
                };

                return Ok(packagingDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackagingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Packaging/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackaging(int id)
        {
            var packaging = await _context.Packaging.FindAsync(id);
            if (packaging == null)
            {
                return NotFound();
            }

            _context.Packaging.Remove(packaging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackagingExists(int id)
        {
            return _context.Packaging.Any(e => e.Id == id);
        }
    }
}


