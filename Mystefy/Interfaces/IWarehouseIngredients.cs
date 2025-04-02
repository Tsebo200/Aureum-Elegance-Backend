using System.Threading.Tasks;
using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IWarehouseIngredients
    {
        Task<WarehouseIngredients> CreateIngredientAsync(WarehouseIngredients ingredient);
        Task<WarehouseIngredients?> GetIngredientWithDetailsAsync(int ingredientId);
        Task<WarehouseIngredients?> GetIngredientByNameAsync(string ingredientName);
        Task<WarehouseIngredients?> UpdateIngredientAsync(WarehouseIngredients ingredient);
        Task<WarehouseIngredients?> DeleteIngredientAsync(int ingredientId);
    }
}
