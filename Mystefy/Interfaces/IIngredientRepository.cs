using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IIngredientRepository
    {
        Task<Ingredients> CreateIngredientAsync(Ingredients ingredient);
        Task<Ingredients?> GetIngredientWithDetails(int ingredientId);
        Task<Ingredients?> GetIngredientWithRecipes(int ingredientId);
        Task AddIngredientToRecipe(int ingredientId, int recipeId);
        Task<Ingredients?> GetIngredientByName(string ingredientName);
        Task<Ingredients?> GetIngredientByType(string ingredientType);
        Task<Ingredients?> GetIngredientByCost(string ingredientCost);
        Task<Ingredients?> GetIngredientByIsExpired(bool isExpired);
        Task<Ingredients?> UpdateIngredient(Ingredients ingredient);
        Task<Ingredients?> DeleteIngredient(int ingredientId);
        Task RemoveIngredientFromRecipe(int ingredientId, int recipeId);
    }
}

