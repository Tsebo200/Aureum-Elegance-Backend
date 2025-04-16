using Mystefy.Interfaces;
using Mystefy.Models;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;

namespace Mystefy.Services
{
    public class WasteLossRecordBatchFinishedProductsRepositoryService : IWasteLossRecordBatchFinishedProductsRepository
    {
        private readonly MystefyDbContext _context;

        public WasteLossRecordBatchFinishedProductsRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<WasteLossRecordBatchFinishedProducts>> GetAllWasteLossRecordsAsync()
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<WasteLossRecordBatchFinishedProducts?> GetWasteLossRecordByIdAsync(int recordId)
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == recordId);
        }

        public async Task<WasteLossRecordBatchFinishedProducts> CreateWasteLossRecordAsync(WasteLossRecordBatchFinishedProducts record)
        {
            _context.WasteLossRecordBatchFinishedProducts.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<WasteLossRecordBatchFinishedProducts?> UpdateWasteLossRecordAsync(WasteLossRecordBatchFinishedProducts record)
        {
            var existingRecord = await _context.WasteLossRecordBatchFinishedProducts.FindAsync(record.Id);
            if (existingRecord == null) return null;

            existingRecord.QuantityLoss = record.QuantityLoss;
            existingRecord.Reason = record.Reason;
            existingRecord.DateOfLoss = record.DateOfLoss;
            existingRecord.UserId = record.UserId;
            existingRecord.ProductId = record.ProductId;
            existingRecord.BatchId = record.BatchId;
            existingRecord.WarehouseId = record.WarehouseId;

            await _context.SaveChangesAsync();
            return existingRecord;
        }

        public async Task<WasteLossRecordBatchFinishedProducts?> DeleteWasteLossRecordAsync(int recordId)
        {
            var record = await _context.WasteLossRecordBatchFinishedProducts.FindAsync(recordId);
            if (record == null) return null;

            _context.WasteLossRecordBatchFinishedProducts.Remove(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByUserIdAsync(int userId)
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByProductIdAsync(int productId)
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByBatchIdAsync(int batchId)
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Where(r => r.BatchId == batchId)
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordBatchFinishedProducts>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WasteLossRecordBatchFinishedProducts
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.FinishedProduct)
                .Include(r => r.Batch)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }
    }
}
