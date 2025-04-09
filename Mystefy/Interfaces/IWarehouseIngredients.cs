using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    // Defines a contract for handling warehouse ingredient-related operations
    public interface IWarehouseIngredients
    {
        // Creates a new warehouse ingredient asynchronously
        Task<WarehouseIngredients> CreateIngredientAsync(WarehouseIngredients ingredient);

        // Retrieves a warehouse ingredient with all its related details
        Task<WarehouseIngredients?> GetIngredientWithDetailsAsync(int ingredientId);

        // Retrieves a warehouse ingredient by its name
        Task<WarehouseIngredients?> GetIngredientByNameAsync(string ingredientName);

        // Updates an existing warehouse ingredient asynchronously
        Task<WarehouseIngredients?> UpdateIngredientAsync(WarehouseIngredients ingredient);

        // Deletes a warehouse ingredient by its ID
        Task<WarehouseIngredients?> DeleteIngredientAsync(int ingredientId);
    }
}

