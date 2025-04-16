using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class DeliveryIngredientsRepository : IDeliveryIngredientsRepository
    {
        private readonly MystefyDbContext _context;

        public DeliveryIngredientsRepository(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryIngredients> CreateDeliveryIngredientsAsync(DeliveryIngredients deliveryIngredient)
        {
            var newRecord = await _context.DeliveryIngredients.AddAsync(deliveryIngredient);
            await _context.SaveChangesAsync();
            return newRecord.Entity;
        }

        public async Task<DeliveryIngredients?> GetDeliveryIngredientAsync(int deliveryIngredientID, int ingredientID)
        {
            return await _context.DeliveryIngredients
                .Include(di => di.Delivery)
                .Include(di => di.Ingredient)
                .FirstOrDefaultAsync(di => di.DeliveryIngredientID == deliveryIngredientID && di.IngredientID == ingredientID);
        }

        public async Task<List<DeliveryIngredients>> GetAllDeliveryIngredientsAsync()
        {
            return await _context.DeliveryIngredients
                .Include(di => di.Delivery)
                .Include(di => di.Ingredient)
                .ToListAsync();
        }

        public async Task<DeliveryIngredients?> UpdateDeliveryIngredientsAsync(DeliveryIngredients deliveryIngredient)
        {
            var existing = await _context.DeliveryIngredients
                .FirstOrDefaultAsync(di => di.DeliveryIngredientID == deliveryIngredient.DeliveryIngredientID && di.IngredientID == deliveryIngredient.IngredientID);
            if (existing == null)
                return null;

            // Update properties as needed. For example:
            existing.QuantityDelivered = deliveryIngredient.QuantityDelivered;
            existing.DateOrdered = deliveryIngredient.DateOrdered;
            existing.DeliveryIngredientCost = deliveryIngredient.DeliveryIngredientCost;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDeliveryIngredientsAsync(int deliveryIngredientID, int ingredientID)
        {
            var existing = await _context.DeliveryIngredients
                .FirstOrDefaultAsync(di => di.DeliveryIngredientID == deliveryIngredientID && di.IngredientID == ingredientID);
            if (existing == null)
                return false;

            _context.DeliveryIngredients.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

