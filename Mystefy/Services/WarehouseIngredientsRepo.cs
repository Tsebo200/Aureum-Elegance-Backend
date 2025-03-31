using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.Models;
using Mystefy.Data;
using Microsoft.EntityFrameworkCore;
using Mystefy
.Interfaces;

namespace Mystefy.Services
{
    public class WarehouseIngredientsRepo : IWarehouseIngredients
    {
        private readonly MystefyDbContext _context;

        public WarehouseIngredientsRepo(MystefyDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseIngredients> CreateIngredientAsync(WarehouseIngredients ingredient)
        {
            await _context.WarehouseIngredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<WarehouseIngredients?> GetIngredientWithDetailsAsync(int ingredientId)
        {
            return await _context.WarehouseIngredients.FindAsync(ingredientId);
        }

        public async Task<WarehouseIngredients?> GetIngredientByNameAsync(string ingredientName)
        {
            return await _context.WarehouseIngredients.FirstOrDefaultAsync(i => i.IngredientName == ingredientName);
        }

        public async Task<WarehouseIngredients?> UpdateIngredientAsync(WarehouseIngredients ingredient)
        {
            _context.WarehouseIngredients.Update(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<WarehouseIngredients?> DeleteIngredientAsync(int ingredientId)
        {
            var ingredient = await GetIngredientWithDetailsAsync(ingredientId);
            if (ingredient != null)
            {
                _context.WarehouseIngredients.Remove(ingredient);
                await _context.SaveChangesAsync();
                return ingredient;
            }
            return null;
        }

        public async Task<List<WarehouseIngredients>> GetAllIngredientsAsync()
        {
            return await _context.WarehouseIngredients.ToListAsync();
        }
    }
}

