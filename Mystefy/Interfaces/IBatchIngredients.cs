//The IBatchIngredientsRepository interface specifies all CRUD operations for BatchIngredients.
using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IBatchIngredientsRepository
    {
        Task<BatchIngredients> CreateBatchIngredientAsync(BatchIngredients batchIngredient);
        Task<BatchIngredients?> GetBatchIngredientAsync(int batchID, int ingredientsID);
        Task<IEnumerable<BatchIngredients>> GetAllBatchIngredientsAsync();
        Task<BatchIngredients?> UpdateBatchIngredientAsync(BatchIngredients batchIngredient);
        Task<bool> DeleteBatchIngredientAsync(int batchID, int ingredientsID);
    }
}
// This interface defines the contract for batch ingredient operations, including creating, retrieving, updating, and deleting batch ingredients. It also includes a method to retrieve all batch ingredients.
// The methods are asynchronous, allowing for non-blocking operations, which is particularly useful in web applications where responsiveness is key. The use of generics allows for flexibility in the types of batch ingredients that can be handled.
