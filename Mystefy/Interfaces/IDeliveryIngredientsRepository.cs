using System.Collections.Generic;
using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    /// Defines a contract for handling delivery-ingredient related database operations.
    public interface IDeliveryIngredientsRepository
    {
        /// Creates a new DeliveryIngredients record asynchronously.
        Task<DeliveryIngredients> CreateDeliveryIngredientsAsync(DeliveryIngredients deliveryIngredient);

        /// Retrieves a specific DeliveryIngredients record given its composite key
        /// (DeliveryIngredientID and IngredientID) asynchronously.
        Task<DeliveryIngredients?> GetDeliveryIngredientAsync(int deliveryIngredientID, int ingredientID);

        /// Retrieves all DeliveryIngredients records asynchronously.
        Task<List<DeliveryIngredients>> GetAllDeliveryIngredientsAsync();

        /// Updates an existing DeliveryIngredients record asynchronously.
        Task<DeliveryIngredients?> UpdateDeliveryIngredientsAsync(DeliveryIngredients deliveryIngredient);

        /// Deletes a DeliveryIngredients record given its composite key asynchronously.
        /// Returns true if the record was deleted successfully.
        Task<bool> DeleteDeliveryIngredientsAsync(int deliveryIngredientID, int ingredientID);
    }
}
//This interface defines the contract for delivery-ingredient operations, including creating, retrieving, updating, and deleting delivery ingredients.
