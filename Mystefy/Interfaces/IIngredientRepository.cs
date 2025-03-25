using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IIngredientRepository
    {
        Task<Ingredients> CreateIngredientAsync(Ingredients ingredient);
        Task<Ingredients?> GetIngredientWithDetailsAsync(int ingredientId);
        Task<Ingredients?> GetIngredientWithRecipesAsync(int ingredientId);
        Task AddIngredientToRecipeAsync(int ingredientId, int recipeId);
        Task<Ingredients?> GetIngredientByNameAsync(string ingredientName);
        Task<Ingredients?> GetIngredientByTypeAsync(string ingredientType);
        Task<Ingredients?> GetIngredientByCostAsync(string ingredientCost);
        Task<Ingredients?> GetIngredientByIsExpiredAsync(bool isExpired);
        Task<Ingredients?> UpdateIngredientAsync(Ingredients ingredient);
        Task<Ingredients?> DeleteIngredientAsync(int ingredientId);
        Task RemoveIngredientFromRecipeAsync(int ingredientId, int recipeId);
    }
}

