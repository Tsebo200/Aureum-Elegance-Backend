using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{ /// <summary>
    /// Service layer handling data operations for StockRequestPackagings.
    /// Implements IStockRequestPackagingsRepository interface.
    /// </summary>
    public class StockRequestPackagingsRepositoryService : IStockRequestPackagingsRepository
    {
        private readonly MystefyDbContext _context;

        public StockRequestPackagingsRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockRequestPackagings>> GetAllStockRequestPackagingsAsync()
        {
            return await _context.StockRequestPackagings
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<StockRequestPackagings?> GetStockRequestPackagingsByIdAsync(int requestId)
        {
            return await _context.StockRequestPackagings
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        public async Task<StockRequestPackagings> CreateStockRequestPackagingsAsync(StockRequestPackagings request)
        {
            _context.StockRequestPackagings.Add(request);
            await _context.SaveChangesAsync();

            await _context.Entry(request).Reference(r => r.User).LoadAsync();
            await _context.Entry(request).Reference(r => r.Packaging).LoadAsync();
            await _context.Entry(request).Reference(r => r.Warehouse).LoadAsync();

            return request;
        }

        public async Task<StockRequestPackagings?> UpdateStockRequestPackagingsAsync(StockRequestPackagings request)
        {
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(request).Reference(r => r.User).LoadAsync();
                await _context.Entry(request).Reference(r => r.Packaging).LoadAsync();
                await _context.Entry(request).Reference(r => r.Warehouse).LoadAsync();

                return request;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StockRequestPackagingsExists(request.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<StockRequestPackagings?> DeleteStockRequestPackagingsAsync(int requestId)
        {
            var request = await _context.StockRequestPackagings.FindAsync(requestId);
            if (request == null)
                return null;

            _context.StockRequestPackagings.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<List<StockRequestPackagings>> GetRequestsByUserIdAsync(int userId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<StockRequestPackagings>> GetRequestsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<StockRequestPackagings>> GetRequestsByPackagingIdAsync(int packagingId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.PackagingId == packagingId)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<StockRequestPackagings>> GetRequestsByStatusAsync(StockRequestPackagings.StockPackagingStatus status)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.Status == status)
                .Include(r => r.User)
                .Include(r => r.Packaging)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        private async Task<bool> StockRequestPackagingsExists(int id)
        {
            return await _context.StockRequestPackagings.AnyAsync(e => e.Id == id);
        }
    }
}
