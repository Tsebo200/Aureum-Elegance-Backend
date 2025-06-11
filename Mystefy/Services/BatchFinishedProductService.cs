using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class BatchFinishedProductService : IBatchFinishedProductService
    {
        private readonly Data.MystefyDbContext _context;

        public BatchFinishedProductService(Data.MystefyDbContext context)
        {
            _context = context;
        }

         public async Task<IEnumerable<BatchFinishedProductDTO>> GetAllAsync()
        {
            var entities = await _context.BatchFinishedProducts
                .Include(bfp => bfp.Batch)
                .Include(bfp => bfp.FinishedProduct)
                .ToListAsync();

            return entities.Select(entity => new BatchFinishedProductDTO(entity));
        }

        public async Task<BatchFinishedProductDTO?> GetByIdAsync(int batchId, int productId)
        {
            var entity = await _context.BatchFinishedProducts
                .Include(p => p.Batch)
                .Include(p => p.FinishedProduct)
                .FirstOrDefaultAsync(p => p.BatchID == batchId && p.ProductID == productId);

            return entity == null ? null : new BatchFinishedProductDTO(entity);
        }

        public async Task<bool> CreateAsync(BatchFinishedProductDTO dto)
{
    var entity = new BatchFinishedProduct
    {
        BatchID = dto.BatchID,
        ProductID = dto.ProductID,
        Quantity = dto.Quantity,
        Unit = dto.Unit,
        Status = dto.Status, 
        WarehouseID = dto.WarehouseID
    };

    _context.BatchFinishedProducts.Add(entity);
    await _context.SaveChangesAsync();
    return true;
}

public async Task<bool> UpdateAsync(BatchFinishedProductDTO dto)
{
    var entity = await _context.BatchFinishedProducts
        .FirstOrDefaultAsync(e => e.BatchID == dto.BatchID && e.ProductID == dto.ProductID);

    if (entity == null) return false;

    entity.Quantity = dto.Quantity;
    entity.Unit = dto.Unit;
    entity.Status = dto.Status; 
    entity.WarehouseID = dto.WarehouseID;

    await _context.SaveChangesAsync();
    return true;
}


        public async Task<bool> DeleteAsync(int batchId, int productId)
        {
            var entity = await _context.BatchFinishedProducts.FindAsync(batchId, productId);
            if (entity == null) return false;

            _context.BatchFinishedProducts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
