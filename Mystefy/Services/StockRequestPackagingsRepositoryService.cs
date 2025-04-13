using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    /// <summary>
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

        /// <summary>
        /// Retrieves all stock request packagings with related entities.
        /// </summary>
        public async Task<List<StockRequestPackagings>> GetAllStockRequestPackagingsAsync()
        {
            return await _context.StockRequestPackagings
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new StockRequestPackagings record.
        /// </summary>
        public async Task<StockRequestPackagings> CreateStockRequestPackagingsAsync(StockRequestPackagings request)
        {
            _context.StockRequestPackagings.Add(request);
            await _context.SaveChangesAsync();

            await _context.Entry(request).Reference(r => r.User).LoadAsync();
            await _context.Entry(request).Reference(r => r.Ingredients).LoadAsync();
            await _context.Entry(request).Reference(r => r.Warehouse).LoadAsync();

            return request;
        }

        /// <summary>
        /// Retrieves a StockRequestPackagings record by its Id.
        /// </summary>
        public async Task<StockRequestPackagings?> GetStockRequestPackagingsByIdAsync(int requestId)
        {
            return await _context.StockRequestPackagings
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        /// <summary>
        /// Retrieves stock request packagings by UserId.
        /// </summary>
        public async Task<List<StockRequestPackagings>> GetRequestsByUserIdAsync(int userId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request packagings by WarehouseId.
        /// </summary>
        public async Task<List<StockRequestPackagings>> GetRequestsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request packagings by IngredientId.
        /// </summary>
        public async Task<List<StockRequestPackagings>> GetRequestsByIngredientIdAsync(int ingredientId)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.IngredientsId == ingredientId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request packagings by their status.
        /// </summary>
        public async Task<List<StockRequestPackagings>> GetRequestsByStatusAsync(StockRequestPackagings.StockPackagingStatus status)
        {
            return await _context.StockRequestPackagings
                .Where(r => r.Status == status)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing StockRequestPackagings record.
        /// </summary>
        public async Task<StockRequestPackagings?> UpdateStockRequestPackagingsAsync(StockRequestPackagings request)
        {
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(request).Reference(r => r.User).LoadAsync();
                await _context.Entry(request).Reference(r => r.Ingredients).LoadAsync();
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

        /// <summary>
        /// Deletes a StockRequestPackagings record by Id.
        /// </summary>
        public async Task<StockRequestPackagings?> DeleteStockRequestPackagingsAsync(int requestId)
        {
            var request = await _context.StockRequestPackagings.FindAsync(requestId);
            if (request == null)
            {
                return null;
            }

            _context.StockRequestPackagings.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        /// <summary>
        /// Helper method to check if a StockRequestPackagings record exists.
        /// </summary>
        private async Task<bool> StockRequestPackagingsExists(int id)
        {
            return await _context.StockRequestPackagings.AnyAsync(e => e.Id == id);
        }
    }
}
