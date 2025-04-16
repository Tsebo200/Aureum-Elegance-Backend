using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    /// <summary>
    /// Service layer handling data operations for WasteLossRecordIngredients.
    /// Implements IWasteLossRecordIngredientsRepository interface.
    /// </summary>
    public class WasteLossRecordIngredientsRepositoryService : IWasteLossRecordIngredientsRepository
    {
        private readonly MystefyDbContext _context;

        public WasteLossRecordIngredientsRepositoryService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<List<WasteLossRecordIngredients>> GetAllWasteLossRecordsAsync()
        {
            return await _context.WasteLossRecordIngredients
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<WasteLossRecordIngredients?> GetWasteLossRecordByIdAsync(int recordId)
        {
            return await _context.WasteLossRecordIngredients
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(r => r.Id == recordId);
        }

        public async Task<WasteLossRecordIngredients> CreateWasteLossRecordAsync(WasteLossRecordIngredients record)
        {
            _context.WasteLossRecordIngredients.Add(record);
            await _context.SaveChangesAsync();

            await _context.Entry(record).Reference(r => r.User).LoadAsync();
            await _context.Entry(record).Reference(r => r.Ingredients).LoadAsync();
            await _context.Entry(record).Reference(r => r.Warehouse).LoadAsync();

            return record;
        }

        public async Task<WasteLossRecordIngredients?> UpdateWasteLossRecordAsync(WasteLossRecordIngredients record)
        {
            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await _context.Entry(record).Reference(r => r.User).LoadAsync();
                await _context.Entry(record).Reference(r => r.Ingredients).LoadAsync();
                await _context.Entry(record).Reference(r => r.Warehouse).LoadAsync();

                return record;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WasteLossRecordExists(record.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<WasteLossRecordIngredients?> DeleteWasteLossRecordAsync(int recordId)
        {
            var record = await _context.WasteLossRecordIngredients.FindAsync(recordId);
            if (record == null)
                return null;

            _context.WasteLossRecordIngredients.Remove(record);
            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByUserIdAsync(int userId)
        {
            return await _context.WasteLossRecordIngredients
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByIngredientIdAsync(int ingredientId)
        {
            return await _context.WasteLossRecordIngredients
                .Where(r => r.IngredientsId == ingredientId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        public async Task<List<WasteLossRecordIngredients>> GetWasteLossRecordsByWarehouseIdAsync(int warehouseId)
        {
            return await _context.WasteLossRecordIngredients
                .Where(r => r.WarehouseId == warehouseId)
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                .Include(r => r.Warehouse)
                .ToListAsync();
        }

        private async Task<bool> WasteLossRecordExists(int id)
        {
            return await _context.WasteLossRecordIngredients.AnyAsync(e => e.Id == id);
        }
    }
}
