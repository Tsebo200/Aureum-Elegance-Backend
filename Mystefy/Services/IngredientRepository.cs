using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly MystefyDbContext _context;

        public IngredientRepository(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<Ingredients> CreateIngredientAsync(Ingredients ingredient)
        {
            var newIngredient = await _context.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return newIngredient.Entity;
        }

        // Renamed to reflect Batch instead of Recipes.
        public async Task<Ingredients?> GetIngredientWithBatchAsync(int ingredientId)
        {
            // Adjust the Include below based on your data model.
            return await _context.Ingredients
                .Include(i => i.StockRequests) // Update or replace with your Batch relationship if available.
                .FirstOrDefaultAsync(i => i.Id == ingredientId);
        }

        // Renamed to reflect Batch instead of Recipe.
        public async Task AddIngredientToBatchAsync(int ingredientId, int batchId)
        {
            // Implementation depends on how Batch and Ingredients are related.
            await Task.CompletedTask; // Ensures at least one await exists.
            throw new NotImplementedException("AddIngredientToBatchAsync is not implemented yet.");
        }

        public async Task<Ingredients?> GetIngredientByNameAsync(string ingredientName)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredientName);
        }

        public async Task<Ingredients?> GetIngredientByTypeAsync(string ingredientType)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Type == ingredientType);
        }

        public async Task<Ingredients?> GetIngredientByCostAsync(string ingredientCost)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Cost == ingredientCost);
        }

        public async Task<Ingredients?> GetIngredientByIsExpiredAsync(bool isExpired)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.IsExpired == isExpired);
        }

        public async Task<Ingredients?> UpdateIngredientAsync(Ingredients ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<Ingredients?> DeleteIngredientAsync(int ingredientId)
        {
            var ingredient = await _context.Ingredients.FindAsync(ingredientId);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
            return ingredient;
        }

        // Renamed to reflect Batch instead of Recipe.
        public async Task RemoveIngredientFromBatchAsync(int ingredientId, int batchId)
        {
            // Implementation depends on your data model.
            await Task.CompletedTask; // Ensures at least one await exists.
            throw new NotImplementedException("RemoveIngredientFromBatchAsync is not implemented yet.");
        }
    }
}

