using System;
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    // Defines a contract for handling ingredient-related database operations
    public interface IIngredientRepository
    {
        // Creates a new ingredient asynchronously
        Task<Ingredients> CreateIngredientAsync(Ingredients ingredient);

        // Retrieves an ingredient with all its related details
        Task<Ingredients?> GetIngredientWithDetails(int ingredientId);

        // Retrieves an ingredient along with its associated recipes
        Task<Ingredients?> GetIngredientWithRecipes(int ingredientId);

        // Adds an ingredient to a specified recipe
        Task AddIngredientToRecipe(int ingredientId, int recipeId);

        // Retrieves an ingredient by its name
        Task<Ingredients?> GetIngredientByName(string ingredientName);

        // Retrieves an ingredient by its type
        Task<Ingredients?> GetIngredientByType(string ingredientType);

        // Retrieves an ingredient by its cost
        Task<Ingredients?> GetIngredientByCost(string ingredientCost);

        // Retrieves an ingredient based on its expiration status
        Task<Ingredients?> GetIngredientByIsExpired(bool isExpired);

        // Updates an existing ingredient asynchronously
        Task<Ingredients?> UpdateIngredient(Ingredients ingredient);

        // Deletes an ingredient by its ID
        Task<Ingredients?> DeleteIngredient(int ingredientId);

        // Removes an ingredient from a specified recipe
        Task RemoveIngredientFromRecipe(int ingredientId, int recipeId);
    }
}