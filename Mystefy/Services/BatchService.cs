using System;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class BatchService : IBatchService
{
     private readonly MystefyDbContext _context;

        public BatchService(MystefyDbContext context)
        {
            _context = context;
        }
    public async Task<Batch> AddBatch(Batch batch)
    {
        if (batch.ProductionDate.Kind == DateTimeKind.Unspecified)
    {
        batch.ProductionDate = DateTime.SpecifyKind(batch.ProductionDate, DateTimeKind.Utc);
    }
    else
    {
        batch.ProductionDate = batch.ProductionDate.ToUniversalTime();
    }

        _context.Batches.Add(batch);
            await _context.SaveChangesAsync();
            return batch;
    }

    public async Task<bool> DeleteBatch(int id)
    {
          var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return false;
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
            return true;
    }

    public async Task<IEnumerable<Batch>> GetAllBatches()
    {
         return await _context.Batches
        .ToListAsync();
    }

    public Task<Batch?> GetBatchById(int id)
    {
        return _context.Batches
        .Include(b =>b.BatchFinishedProducts)
        .FirstOrDefaultAsync(b => b.BatchID == id);
    }

    public async Task<bool> UpdateBatch(int id, Batch updatedBatch)
    {
         var existingBatch = await _context.Batches.FindAsync(id);
    if (existingBatch == null)
    {
        return false;
    }

    // Ensure ProductionDate is UTC
    if (updatedBatch.ProductionDate.Kind == DateTimeKind.Unspecified)
    {
        updatedBatch.ProductionDate = DateTime.SpecifyKind(updatedBatch.ProductionDate, DateTimeKind.Utc);
    }
    else
    {
        updatedBatch.ProductionDate = updatedBatch.ProductionDate.ToUniversalTime();
    }

    existingBatch.ProductionDate = updatedBatch.ProductionDate;
    existingBatch.BatchSize = updatedBatch.BatchSize;

    _context.Batches.Update(existingBatch);
    await _context.SaveChangesAsync();
    return true;
    }
}
