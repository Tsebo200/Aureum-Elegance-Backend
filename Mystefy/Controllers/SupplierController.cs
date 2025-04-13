using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetAll()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers); // Return the list of suppliers
        }

        // GET: api/supplier/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier == null) return NotFound();  // If not found, return NotFound
            return Ok(supplier);  // Otherwise, return the supplier
        }

        // POST: api/supplier
        [HttpPost]
        public async Task<ActionResult<SupplierDTO>> Create(SupplierDTO dto)
        {
            var created = await _supplierService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SupplierID }, created); // Return created resource with location
        }

        // PUT: api/supplier/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDTO>> Update(int id, SupplierDTO dto)
        {
            var updated = await _supplierService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();  // If update failed (e.g. supplier not found), return NotFound
            return Ok(updated);  // Otherwise, return the updated supplier
        }

        // DELETE: api/supplier/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _supplierService.DeleteAsync(id);
            if (!success) return NotFound();  // If deletion fails (e.g. supplier not found), return NotFound
            return NoContent();  // Otherwise, return NoContent indicating successful deletion
        }
    }
}
