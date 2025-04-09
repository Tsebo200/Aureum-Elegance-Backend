using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatchs()
        {
            return Ok(await _batchService.GetAllBatches());
        }

        // GET: api/Batch/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Batch>> GetBatch(int id)
        {
           var batch = await _batchService.GetBatchById(id);
           return batch == null? NotFound() : Ok(batch);
        }

       
    [HttpPost]
    public async Task<ActionResult<Batch>> PostBatch(Batch batch)
    {
       
        var newBatch= await _batchService.AddBatch(batch);
        return CreatedAtAction("GetBatch", new { id = newBatch.BatchID }, newBatch);
    }


        // PUT: api/Batch/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(int id, Batch batch)
        {
              var updated = await _batchService.UpdateBatch(id, batch);
            
            if(!updated){
                return NotFound();

            };
            return NoContent();
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
