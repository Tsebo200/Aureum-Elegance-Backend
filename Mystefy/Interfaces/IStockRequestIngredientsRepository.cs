using Mystefy.Models;

namespace Mystefy.Interfaces
{
    public interface IStockRequestIngredientsRepository
    {
        Task<List<StockRequestIngredients>> GetAllStockRequestIngredientsAsync();
        Task<StockRequestIngredients> CreateStockRequestIngredientsAsync(StockRequestIngredients request);
        Task<StockRequestIngredients?> GetStockRequestIngredientsByIdAsync(int requestId);
        Task<List<StockRequestIngredients>> GetRequestsByUserIdAsync(int userId);
        Task<List<StockRequestIngredients>> GetRequestsByWarehouseIdAsync(int warehouseId);
        Task<List<StockRequestIngredients>> GetRequestsByIngredientIdAsync(int ingredientId);
        Task<List<StockRequestIngredients>> GetRequestsByStatusAsync(StockRequestIngredients.StockStatus status);
        Task<StockRequestIngredients?> UpdateStockRequestIngredientsAsync(StockRequestIngredients request);
        Task<StockRequestIngredients?> DeleteStockRequestIngredientsAsync(int requestId);
    }
}
