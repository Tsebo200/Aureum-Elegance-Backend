using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    /// <summary>
    /// Service layer handling data operations for StockRequestIngredients.
    /// Implements IStockRequestIngredientsRepository interface.
    /// </summary>
    public class StockRequestIngredientsRepositoryService : IStockRequestIngredientsRepository
    {
        private readonly MystefyDbContext _context;

        public StockRequestIngredientsRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all stock request ingredients with related entities.
        /// </summary>
        public async Task<List<StockRequestIngredients>> GetAllStockRequestIngredientsAsync()
        {
            return await _context.StockRequestIngredients
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new StockRequestIngredients record.
        /// </summary>
        public async Task<StockRequestIngredients> CreateStockRequestIngredientsAsync(StockRequestIngredients request)
        {
            _context.StockRequestIngredients.Add(request);
            await _context.SaveChangesAsync();

            await _context.Entry(request).Reference(r => r.User).LoadAsync();
            await _context.Entry(request).Reference(r => r.Ingredients).LoadAsync();
            await _context.Entry(request).Reference(r => r.Warehouse).LoadAsync();

            return request;
        }

        /// <summary>
        /// Retrieves a StockRequestIngredients record by its Id.
        /// </summary>
        public async Task<StockRequestIngredients?> GetStockRequestIngredientsByIdAsync(int requestId)
        {
            return await _context.StockRequestIngredients
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }

        /// <summary>
        /// Retrieves stock request ingredients by UserId.
        /// </summary>
        public async Task<List<StockRequestIngredients>> GetRequestsByUserIdAsync(int userId)
        {
            return await _context.StockRequestIngredients
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request ingredients by WarehouseId.
        /// </summary>
        public async Task<List<StockRequestIngredients>> GetRequestsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.StockRequestIngredients
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request ingredients by IngredientId.
        /// </summary>
        public async Task<List<StockRequestIngredients>> GetRequestsByIngredientIdAsync(int ingredientId)
        {
            return await _context.StockRequestIngredients
                .Where(r => r.IngredientsId == ingredientId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves stock request ingredients by their status.
        /// </summary>
        public async Task<List<StockRequestIngredients>> GetRequestsByStatusAsync(StockRequestIngredients.StockStatus status)
        {
            return await _context.StockRequestIngredients
                .Where(r => r.Status == status)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing StockRequestIngredients record.
        /// </summary>
        public async Task<StockRequestIngredients?> UpdateStockRequestIngredientsAsync(StockRequestIngredients request)
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
                if (!await StockRequestIngredientsExists(request.Id))
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
        /// Deletes a StockRequestIngredients record by Id.
        /// </summary>
        public async Task<StockRequestIngredients?> DeleteStockRequestIngredientsAsync(int requestId)
        {
            var request = await _context.StockRequestIngredients.FindAsync(requestId);
            if (request == null)
            {
                return null;
            }

            _context.StockRequestIngredients.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        /// <summary>
        /// Helper method to check if a StockRequestIngredients record exists.
        /// </summary>
        private async Task<bool> StockRequestIngredientsExists(int id)
        {
            return await _context.StockRequestIngredients.AnyAsync(e => e.Id == id);
        }
    }
}
