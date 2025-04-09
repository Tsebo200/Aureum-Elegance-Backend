//The IBatchIngredientsRepository interface specifies all CRUD operations for BatchIngredients.
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    // Defines a contract for handling ingredient-related database operations.
    public interface IIngredientRepository
    {
        // Creates a new ingredient asynchronously.
        Task<Ingredients> CreateIngredientAsync(Ingredients ingredient);

        // Retrieves an ingredient along with all its related details (Delivery, Fragrance, 
        // StockRequests, WasteLossRecords and WarehouseIngredients) asynchronously.
        Task<Ingredients?> GetIngredientWithDetailsAsync(int ingredientId);

        // Retrieves an ingredient by its name asynchronously.
        Task<Ingredients?> GetIngredientByNameAsync(string ingredientName);

        // Retrieves an ingredient by its type asynchronously.
        Task<Ingredients?> GetIngredientByTypeAsync(string ingredientType);

        // Retrieves an ingredient by its cost asynchronously.
        Task<Ingredients?> GetIngredientByCostAsync(string ingredientCost);

        // Retrieves an ingredient based on its expiration status asynchronously.
        Task<Ingredients?> GetIngredientByIsExpiredAsync(bool isExpired);

        // Updates an existing ingredient asynchronously.
        Task<Ingredients?> UpdateIngredientAsync(Ingredients ingredient);

        // Deletes an ingredient by its ID asynchronously.
        Task<Ingredients?> DeleteIngredientAsync(int ingredientId);

        // Retrieves all ingredients asynchronously.
        Task<IEnumerable<Ingredients>> GetAllIngredientsAsync();
    }
}

