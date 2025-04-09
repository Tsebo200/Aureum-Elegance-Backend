using Microsoft.EntityFrameworkCore;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services
{
    public class FragranceIngredientService : IFragranceIngredientService
    {
        private readonly MystefyDbContext _context;

        public FragranceIngredientService(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<FragranceIngredient> AddFragranceIngredient(FragranceIngredient fragranceIngredient)
        {
            _context.FragranceIngredients.Add(fragranceIngredient);
            await _context.SaveChangesAsync();
            return fragranceIngredient;
        }

        public async Task<bool> DeleteFragranceIngredient(int fragranceId, int ingredientId)
        {
            var ingredient = await _context.FragranceIngredients
                .FirstOrDefaultAsync(fi => fi.FragranceID == fragranceId && fi.IngredientsID == ingredientId);

            if (ingredient == null) return false;

            _context.FragranceIngredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FragranceIngredient>> GetAllFragranceIngredients()
        {
            return await _context.FragranceIngredients
                .Include(fi => fi.Fragrance)
                .Include(fi => fi.Ingredients)
                .ToListAsync();
        }

        public async Task<FragranceIngredient?> GetFragranceIgredientsById(int fragranceId, int ingredientId)
        {
            return await _context.FragranceIngredients
                .Include(fi => fi.Fragrance)
                .Include(fi => fi.Ingredients)
                .FirstOrDefaultAsync(fi => fi.FragranceID == fragranceId && fi.IngredientsID == ingredientId);
        }

        public async Task<bool> UpdateFragranceIngredient(int fragranceId, int ingredientId, FragranceIngredient updatedIngredient)
        {
            var existing = await _context.FragranceIngredients
                .FirstOrDefaultAsync(fi => fi.FragranceID == fragranceId && fi.IngredientsID == ingredientId);

            if (existing == null) return false;

            // Update properties
            existing.Amount = updatedIngredient.Amount;
            existing.FragranceID = updatedIngredient.FragranceID;
            existing.IngredientsID = updatedIngredient.IngredientsID;

            _context.Entry(existing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}
