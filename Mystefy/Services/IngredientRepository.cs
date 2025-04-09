using System.Collections.Generic;
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
            var newIngredient = await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return newIngredient.Entity;
        }

        public async Task<Ingredients?> GetIngredientWithDetailsAsync(int ingredientId)
        {
            return await _context.Ingredients
                .Include(i => i.DeliveryIngredients)
                .Include(i => i.FragranceIngredients)
                .Include(i => i.StockRequests)
                //.Include(i => i.WasteLossRecords)
                .Include(i => i.WarehouseIngredients)
                .FirstOrDefaultAsync(i => i.Id == ingredientId);
        }

        public async Task<Ingredients?> GetIngredientByNameAsync(string ingredientName)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Name == ingredientName);
        }

        public async Task<Ingredients?> GetIngredientByTypeAsync(string ingredientType)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Type == ingredientType);
        }

        public async Task<Ingredients?> GetIngredientByCostAsync(string ingredientCost)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Cost == ingredientCost);
        }

        public async Task<Ingredients?> GetIngredientByIsExpiredAsync(bool isExpired)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.IsExpired == isExpired);
        }

        public async Task<Ingredients?> UpdateIngredientAsync(Ingredients ingredient)
        {
            var existing = await _context.Ingredients.FindAsync(ingredient.Id);
            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(ingredient);
            await _context.SaveChangesAsync();
            return existing;
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

        public async Task<List<Ingredients>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        Task<IEnumerable<Ingredients>> IIngredientRepository.GetAllIngredientsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
