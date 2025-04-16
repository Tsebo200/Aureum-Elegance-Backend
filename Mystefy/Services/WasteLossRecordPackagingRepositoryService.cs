using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class WasteLossRecordPackagingRepositoryService : IWasteLossRecordPackagingRepository
    {
        private readonly MystefyDbContext _context;

        public WasteLossRecordPackagingRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<WasteLossRecordPackaging>> GetAllWasteLossRecordsAsync()
        {
            return await _context.WasteLossRecordPackaging
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<WasteLossRecordPackaging?> GetWasteLossRecordByIdAsync(int recordId)
        {
            return await _context.WasteLossRecordPackaging
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == recordId);
        }

        public async Task<WasteLossRecordPackaging> CreateWasteLossRecordAsync(WasteLossRecordPackaging record)
        {
            _context.WasteLossRecordPackaging.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<WasteLossRecordPackaging?> UpdateWasteLossRecordAsync(WasteLossRecordPackaging record)
        {
            var existingRecord = await _context.WasteLossRecordPackaging.FindAsync(record.Id);
            if (existingRecord == null)
                return null;

            existingRecord.QuantityLoss = record.QuantityLoss;
            existingRecord.Reason = record.Reason;
            existingRecord.DateOfLoss = record.DateOfLoss;
            existingRecord.UserId = record.UserId;
            existingRecord.PackagingId = record.PackagingId;
            existingRecord.WarehouseId = record.WarehouseId;

            await _context.SaveChangesAsync();
            return existingRecord;
        }

        public async Task<WasteLossRecordPackaging?> DeleteWasteLossRecordAsync(int recordId)
        {
            var record = await _context.WasteLossRecordPackaging.FindAsync(recordId);
            if (record == null)
                return null;

            _context.WasteLossRecordPackaging.Remove(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByUserIdAsync(int userId)
        {
            return await _context.WasteLossRecordPackaging
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByPackagingIdAsync(int packagingId)
        {
            return await _context.WasteLossRecordPackaging
                .Where(r => r.PackagingId == packagingId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordPackaging>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WasteLossRecordPackaging
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }
    }
}
