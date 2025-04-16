using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }
        // GET: api/Batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchWithFinishedProductDTO>>> GetBatchs()
        {
            var getBatch = await _batchService.GetAllBatches();

             var BatchDtos = getBatch.Select( bwfp=> new BatchWithFinishedProductDTO
            {
                BatchID = bwfp.BatchID,
                ProductionDate= bwfp.ProductionDate,
                BatchSize = bwfp.BatchSize,
                Status = bwfp.Status.ToString(),
                BatchFinishedProducts = bwfp.BatchFinishedProducts?.Select(bf => new BatchFinishedProductInBatchDTO
                {
                    ProductID = bf.ProductID,
                    Quantity= bf.Quantity,
                    Unit = bf.Unit,
                    Status = bf.Status,
                    WarehouseID = bf.WarehouseID
                }).ToList()
            }).ToList();

            return Ok(BatchDtos);
        }

        // GET: api/Batch/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchWithFinishedProductDTO>> GetBatch(int id)
        {
           var batch = await _batchService.GetBatchById(id);

            if (batch == null)
                return NotFound();

            var batchDto = new BatchWithFinishedProductDTO
            {
                BatchID = batch.BatchID,
                ProductionDate = batch.ProductionDate,
                BatchSize = batch.BatchSize,
                Status = batch.Status.ToString(),
                BatchFinishedProducts = batch.BatchFinishedProducts?.Select(fp => new BatchFinishedProductInBatchDTO
                {
                    ProductID = fp.ProductID,
                    Quantity = fp.Quantity,
                    Unit = fp.Unit,
                    Status = fp.Status,
                    WarehouseID = fp.WarehouseID
                }).ToList()
            };

            return Ok(batchDto);
        }

       
    [HttpPost]
    public async Task<ActionResult<Batch>> PostBatch( [FromBody] BatchDTO batchDto)
    {
          if (batchDto == null)
    {
        return BadRequest("Batch data is required.");
    }

    // Convert status string to enum
    if (!Enum.TryParse<BatchStatus>(batchDto.Status, true, out var statusEnum))
    {
        return BadRequest("Invalid status value.");
    }

    // Map DTO to entity
    var batch = new Batch
    {
        ProductionDate = DateTime.SpecifyKind(batchDto.ProductionDate, DateTimeKind.Utc),
        BatchSize = batchDto.BatchSize,
        Status = statusEnum
    };

    var newBatch = await _batchService.AddBatch(batch);

    return CreatedAtAction("GetBatch", new { id = newBatch.BatchID }, newBatch);
    }


        // PUT: api/Batch/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(int id, [FromBody] BatchDTO batchDto)
        {
            if (batchDto == null)
            {
                return BadRequest("Batch data is required.");
            }

            var existingBatch = await _batchService.GetBatchById(id);
            
            if (existingBatch == null)
            {
                return NotFound();
            }

            // Convert status string to enum
            if (!Enum.TryParse<BatchStatus>(batchDto.Status, true, out var statusEnum))
            {
                return BadRequest("Invalid status value.");
            }

            // Update entity with DTO values
            existingBatch.ProductionDate = DateTime.SpecifyKind(batchDto.ProductionDate, DateTimeKind.Utc);
            existingBatch.BatchSize = batchDto.BatchSize;
            existingBatch.Status = statusEnum;

            var updated = await _batchService.UpdateBatch(id, existingBatch);

            return updated ? NoContent() : StatusCode(500, "Failed to update the batch.");
            }

        // DELETE: api/Batch/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
             var deleted = await _batchService.DeleteBatch(id);
            if(!deleted) 
            {
                return NotFound();
            };

            return NoContent();
        }

        
    }
}
