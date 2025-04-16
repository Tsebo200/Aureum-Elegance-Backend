using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;
using Microsoft.EntityFrameworkCore;

namespace Mystefy.Services
{
    public class WasteLossRecordFragranceRepositoryService : IWasteLossRecordFragranceRepository
    {
        private readonly MystefyDbContext _context;

        public WasteLossRecordFragranceRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<WasteLossRecordFragrance>> GetAllWasteLossRecordsAsync()
        {
            return await _context.WasteLossRecordFragrance
                .Include(w => w.User)
                .Include(w => w.Fragrance)
                .Include(w => w.Warehouse)
                .ToListAsync();
        }

        public async Task<WasteLossRecordFragrance?> GetWasteLossRecordByIdAsync(int recordId)
        {
            return await _context.WasteLossRecordFragrance
                .Include(w => w.User)
                .Include(w => w.Fragrance)
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(w => w.Id == recordId);
        }

        public async Task<WasteLossRecordFragrance> CreateWasteLossRecordAsync(WasteLossRecordFragrance record)
        {
            _context.WasteLossRecordFragrance.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<WasteLossRecordFragrance?> UpdateWasteLossRecordAsync(WasteLossRecordFragrance record)
        {
            var existingRecord = await _context.WasteLossRecordFragrance.FindAsync(record.Id);
            if (existingRecord == null)
            {
                return null;
            }

            existingRecord.QuantityLoss = record.QuantityLoss;
            existingRecord.Reason = record.Reason;
            existingRecord.DateOfLoss = record.DateOfLoss;
            existingRecord.UserId = record.UserId;
            existingRecord.FragranceId = record.FragranceId;
            existingRecord.WarehouseId = record.WarehouseId;

            await _context.SaveChangesAsync();
            return existingRecord;
        }

        public async Task<WasteLossRecordFragrance?> DeleteWasteLossRecordAsync(int recordId)
        {
            var record = await _context.WasteLossRecordFragrance.FindAsync(recordId);
            if (record == null)
            {
                return null;
            }

            _context.WasteLossRecordFragrance.Remove(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByUserIdAsync(int userId)
        {
            return await _context.WasteLossRecordFragrance
                .Where(w => w.UserId == userId)
                .Include(w => w.User)
                .Include(w => w.Fragrance)
                .Include(w => w.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByFragranceIdAsync(int fragranceId)
        {
            return await _context.WasteLossRecordFragrance
                .Where(w => w.FragranceId == fragranceId)
                .Include(w => w.User)
                .Include(w => w.Fragrance)
                .Include(w => w.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordFragrance>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WasteLossRecordFragrance
                .Where(w => w.WarehouseId == warehouseId)
                .Include(w => w.User)
                .Include(w => w.Fragrance)
                .Include(w => w.Warehouse)
                .ToListAsync();
        }
    }
}
